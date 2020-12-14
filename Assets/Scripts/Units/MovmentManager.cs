﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Steamwar.Utils;

namespace Steamwar.Units
{
    public class MovmentManager
    {
        public MovementState state;
        public IEnumerable<PathNode> path;
        public PathNode current;
        public UnitBehaviour unit;

        public static void Move(UnitBehaviour unit, IEnumerable<PathNode> path)
        {
            unit.moves = true;
            unit.StartCoroutine(MoveUnit(unit, path));
        }

        public static System.Collections.IEnumerator MoveUnit(UnitBehaviour unit, IEnumerable<PathNode> path)
        {
            Transform transform = unit.transform;
            foreach (PathNode point in path)
            {
                Vector2 delta = ((Vector2)transform.position) - point.position;
                if(delta.x > 0)
                {
                    unit.facingDirection = Direction.RIGHT;
                }
                else if(delta.y < 0)
                {
                    unit.facingDirection = Direction.UP;
                }
                else if(delta.y > 0)
                {
                    unit.facingDirection = Direction.DOWN;
                }
                else if(delta.x < 0)
                {
                    unit.facingDirection = Direction.LEFT;
                }
                unit.UpdateAnimation();
                float sqrDistance = (((Vector2)transform.position) - point.position).sqrMagnitude;
                while (sqrDistance > float.Epsilon && ((Vector2)transform.position) != point.position)
                {
                    transform.position = Vector2.MoveTowards(transform.position, point.position,  1F / unit.data.type.Speed * Time.deltaTime);
                    sqrDistance = (((Vector2)transform.position) - point.position).sqrMagnitude;
                    yield return new WaitForEndOfFrame();
                }
            }
            unit.moves = false;
            unit.UpdateAnimation();
        }

        public enum MovementState
        {
            PRE,
            MOVING,
            END
        }
    }
}
