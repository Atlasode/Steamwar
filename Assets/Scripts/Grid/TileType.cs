﻿using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

using Steamwar.Utils;

namespace Steamwar.Grid
{
    public class TileType : Tile
    {
        public string id;
        public string displayName;
        public bool chessable;

#if UNITY_EDITOR
        [MenuItem("Create/Tile")]
        static void CreateType()
        {
            ScriptableObjectUtility.CreateAsset<TileType>();
        }
#endif
    }
}