﻿using System;
using System.Collections.Generic;
using Steamwar.Units;
using Steamwar.Buildings;
using Steamwar.Sectors;
using Steamwar.Objects;
using UnityEngine;
using System.Collections;
using Steamwar.Supplies;
using UnityEngine.Tilemaps;

namespace Steamwar.Utils
{
    public class Registry : IService
    {
        private readonly Dictionary<ObjectKind, Dictionary<string, ObjectType>> objects = new Dictionary<ObjectKind, Dictionary<string, ObjectType>>();
        private readonly Dictionary<string, Sector> sectors = new Dictionary<string, Sector>();
        private readonly Dictionary<string, Supply> resources = new Dictionary<string, Supply>();

        public Registry()
        {
            LoadAssets();
        }

        private void LoadAssets()
        {
            foreach (ObjectType type in ScriptableObjectUtility.GetAllInstances<ObjectType>())
            {
                objects.AddIfAbsent(type.kind, () => new Dictionary<string, ObjectType>())[type.id] = type;
            }
            foreach (Sector type in ScriptableObjectUtility.GetAllInstances<Sector>())
            {
                sectors[type.id] = type;
            }
            foreach (Supply type in ScriptableObjectUtility.GetAllInstances<Supply>())
            {
                resources[type.id] = type;
            }
        }


        public ObjectType GetType(string typeId, ObjectKind kind)
        {
            if (typeId == null || typeId.Length == 0)
            {
                typeId = "missing";
            }
            if (objects.Count == 0)
            {
                LoadAssets();
            }
            return objects[kind][typeId];
        }

        public IEnumerable<ObjectType> GetTypes(ObjectKind kind)
        {
            return objects[kind].Values;
        }

        public IEnumerable<Supply> GetResources()
        {
            return resources.Values;
        }

        public Sector GetSector(string name)
        {
            return sectors[name];
        }

        public Supply GetResource(string name)
        {
            return resources[name];
        }

        public static string GetName(TileBase tile)
        {
            return tile.name.ToLower().Replace(' ', '_');
        }

        public IEnumerator Initialize()
        {
            yield return null;
        }

        public IEnumerator CleanUp()
        {
            yield return null;
        }
    }
}
