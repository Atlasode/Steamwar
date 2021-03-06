﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using Steamwar.Utils;
using Steamwar.Units;
using Steamwar.Objects;
using Steamwar.Grid;

namespace Steamwar.Navigation
{
    /// <summary>
    /// A A* implementation for Path finding. 
    /// </summary>
    public class PathFinder
    {

        public readonly Vector2 destination;
        public readonly GameObject unit;
        public readonly Collider2D collider;
        public readonly Vector2 origin;
        public readonly int maxDistance;

        private readonly Dictionary<int, PathNode> points = new Dictionary<int, PathNode>();
        private readonly PathNode[] options = new PathNode[4];

        public PathFinder(Vector2 destination, Vector2 origin, GameObject unit, int maxDistance = 32)
        {
            this.destination = destination;
            this.origin = origin;
            this.unit = unit;
            this.maxDistance = maxDistance;
            this.collider = unit.GetComponent<Collider2D>();

        }

        public PathNode Node(Vector2 position)
        {
            int hash = PathNode.CreateHash(position);
            if (points.ContainsKey(hash))
            {
                return points[hash];
            }
            PathNode node = new PathNode(position);
            points.Add(hash, node);
            return node;
        }

        public Path FindPath()
        {
            PathNode originNode = Node(origin);
            PathNode destinationNode = Node(destination);
            PathQueue heap = new PathQueue();
            heap.Enqueue(originNode);
            HashSet<PathNode> nodes = new HashSet<PathNode>();
            int count = 0;
            while (!heap.Empty())
            {
                if (count > 300)
                {
                    break;
                }
                count++;
                PathNode current = heap.Dequeue();
                if (current == destinationNode)
                {
                    break;
                }
                current.visited = true;
                nodes.Add(current);
                int optionCount = FindOptions(current);
                for (int i = 0; i < optionCount; i++)
                {
                    PathNode option = options[i];
                    NodeType type = GetType(current.position, option.position);
                    option.distanceToDestination = (int)option.position.ManhattanDistance(destinationNode.position);
                    option.distanceToOrigin = current.distanceToOrigin + 1;
                    option.malusFromDistance = current.malusFromDistance + (int) type;
                    int priority = option.malusFromDistance + option.distanceToDestination;
                    if(type == NodeType.BLOCKED || option.distanceToOrigin > maxDistance || option.Assigned() && option.priority <= priority)
                    {
                        continue;
                    }
                    option.type = type;
                    option.previous = current;
                    if (option.Assigned())
                    {
                        heap.UpdatePriority(option, priority);
                    }
                    else
                    {
                        option.priority = priority;
                        heap.Enqueue(option);
                    }
                }
            }
            Stack<PathNode> pathStack = new Stack<PathNode>();
            for(PathNode point = destinationNode;point.previous != null;point=point.previous)
            {
                pathStack.Push(point);
            }
            pathStack.Push(originNode);
            Path path = new Path(pathStack.ToArray());
            nodes.Add(destinationNode);
            //path.nodes = nodes;
            return path;
        }

        public int FindOptions(PathNode current)
        {
            Vector2 position = current.position;
            int count = 0;
            PathNode left = Node(position + Vector2.left);
            PathNode right = Node(position + Vector2.right);
            PathNode up = Node(position + Vector2.up);
            PathNode down = Node(position + Vector2.down);
            void test(PathNode node)
            {
                if (!node.visited)
                {
                    options[count++] = node;
                }
            }
            test(left);
            test(right);
            test(up);
            test(down);

            return count;
        }

        public NodeType GetType(Vector2 from, Vector2 to)
        {
            TileBase tile = BoardManager.GetTileWorld(to);

            RaycastHit2D groundHit = Physics2D.Linecast(from, to, ObjectManager.Instance.groundLayer);
            RaycastHit2D hit = Physics2D.Linecast(to, from, ObjectManager.Instance.groundLayer);
            if (groundHit.collider != null)
            {
                return NodeType.BLOCKED;
            }
            collider.enabled = false;
            RaycastHit2D unitHit = Physics2D.Linecast(from, to, ObjectManager.Instance.unitLayer);
            collider.enabled = true;
            return unitHit.collider == null ? NodeType.WALKABLE : NodeType.BLOCKED;
        }
    }
}
