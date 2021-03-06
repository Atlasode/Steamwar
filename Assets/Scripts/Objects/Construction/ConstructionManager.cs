﻿using UnityEngine;
using UnityEngine.EventSystems;
using System;
using Steamwar.Renderer;
using Steamwar.Buildings;
using Steamwar.Utils;
using Steamwar.Units;
using Steamwar.Interaction;
using UnityEngine.Tilemaps;
using Steamwar.Grid;

namespace Steamwar.Objects
{
    /// <summary>
    /// Helper class to create objects.
    /// </summary>
    public class ConstructionManager : Singleton<ConstructionManager>
    {
        [Header("Prefabs")]
        public GameObject unitPrefab;
        public GameObject buildingPrefab;
        [Header("Object Containers")]
        public GameObject unitContainer;
        public GameObject buildingContainer;

        public static GameObject CreateObjectFromData(ObjectData data)
        {
            GameObject obj = CreateBaseObject(data.Type);
            if (obj != null)
            {
                obj.transform.position = data.Position;
            }
            return obj;
        }

        private GameObject CreateFromPrefab(ObjectType type)
        {
            switch (type.kind)
            {
                case ObjectKind.UNIT:
                    return Instantiate(unitPrefab, unitContainer.transform);
                case ObjectKind.BUILDING:
                    return Instantiate(buildingPrefab, buildingContainer.transform);
                default:
                    GameObject obj = new GameObject(type.id, new Type[] { typeof(SpriteRenderer), typeof(Rigidbody2D) });
                    obj.transform.parent = Instance.transform;
                    SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
                    Rigidbody2D rigidbody = obj.GetComponent<Rigidbody2D>();
                    rigidbody.bodyType = RigidbodyType2D.Kinematic;
                    renderer.sprite = type.spriteBlue;
                    renderer.sortingLayerID = type.kind == ObjectKind.UNIT ? SortingLayer.NameToID("Units") : SortingLayer.NameToID("Objects");
                    renderer.sortingOrder = 0;
                    obj.layer = 8;//Id of the unit layer
                    obj.AddComponent<PolygonCollider2D>();
                    return obj;
            }
        }

        public static GameObject CreateBaseObject(ObjectType type)
        {
            GameObject objInstance = Instance.CreateFromPrefab(type);
            ObjectContainer obj = objInstance.GetComponent<ObjectContainer>();
            if (obj != null)
            {
                obj.OnPrefabInit();
                AddElement(obj, type);
            }
            return objInstance;
        }

        public static void CreateOnPos(Vector2 pos, ObjectType selectedType)
        {
            GameObject unitObj = CreateBaseObject(selectedType);
            unitObj.transform.position = pos;
            ObjectContainer unit = unitObj.GetComponent<ObjectContainer>();
            unit.OnConstruction(selectedType);
        }

        public static void AddElement(MonoBehaviour obj, ObjectType type)
        {
            if (type.elementPrefab != null)
            {
                GameObject element = Instantiate(type.elementPrefab);
                element.name = "Element";
                element.transform.parent = obj.transform;
            }
        }
    }
}
