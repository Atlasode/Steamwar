﻿using System;
using System.Collections.Generic;
using Steamwar.Units;
using Steamwar.Buildings;
using Steamwar.Sectors;
using Steamwar.Objects;
using UnityEngine;
using System.Collections;

namespace Steamwar.Utils
{
    public class Registry
    {
        private readonly Dictionary<string, BuildingType> buildings = new Dictionary<string, BuildingType>();
        private readonly Dictionary<string, UnitType> units = new Dictionary<string, UnitType>();
        private readonly Dictionary<string, Sector> sectors = new Dictionary<string, Sector>();

        public Registry()
        {
            LoadAssets();
        }

        private void LoadAssets()
        {
            foreach (BuildingType type in ScriptableObjectUtility.GetAllInstances<BuildingType>())
            {
                buildings[type.id] = type;
            }
            foreach (UnitType type in ScriptableObjectUtility.GetAllInstances<UnitType>())
            {
                units[type.id] = type;
            }
            foreach (Sector type in ScriptableObjectUtility.GetAllInstances<Sector>())
            {
                sectors[type.id] = type;
            }
        }

        public BuildingType GetBuilding(string name)
        {
            return buildings[name];
        }

        public T GetType<T>(string typeId) where T : class
        {
            if (typeId == null || typeId.Length == 0)
            {
                typeId = "missing";
            }
            if (buildings.Count == 0)
            {
                LoadAssets();
            }
            Type typeType = typeof(T);
            if (typeType == typeof(BuildingType))
            {
                return buildings[typeId] as T;
            }
            else if (typeType == typeof(UnitType))
            {
                return units[typeId] as T;
            }
            return default;
        }

        public IEnumerable<ObjectType> GetTypes(ObjectKind kind)
        {
            if (kind == ObjectKind.BUILDING)
            {
                return buildings.Values;
            }
            else if (kind == ObjectKind.UNIT)
            {
                return units.Values;
            }
            return default;
        }

        public ICollection<T> GetTypes<T>()
        {
            Type typeType = typeof(T);
            if (typeType == typeof(BuildingType))
            {
                return buildings.Values as ICollection<T>;
            }
            else if (typeType == typeof(UnitType))
            {
                return units.Values as ICollection<T>;
            }
            return default;
        }

        public UnitType GetUnit(string name)
        {
            return units[name];
        }

        public Sector GetSector(string name)
        {
            return sectors[name];
        }
    }
}
