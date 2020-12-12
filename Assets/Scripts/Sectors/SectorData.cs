﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Steamwar.Sectors
{
    [Serializable]
    public class SectorData
    {
        [NonSerialized]
        public Sector sector;
        public string sectorId;
        [NonSerialized]
        public Vector4 bounds;

        public SectorData(Sector sector)
        {
            this.sector = sector;
            bounds = new Vector4(-sector.size.x / 2 + sector.center.x + Sector.DEFAULT_POSITION.x,
                -sector.size.y / 2 + sector.center.y + Sector.DEFAULT_POSITION.y,
                sector.size.x / 2 + sector.center.x + Sector.DEFAULT_POSITION.x,
                sector.size.y / 2 + sector.center.y + Sector.DEFAULT_POSITION.y);
            GetHashCode();
        }

        public Vector2 RandomCloudPosition()
        {
            return new Vector2(bounds.x, bounds.y) + Vector2.up * (UnityEngine.Random.value * sector.size.y);
        }
    }
}
