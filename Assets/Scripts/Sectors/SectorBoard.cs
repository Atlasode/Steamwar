﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steamwar.Buildings;
using Steamwar.Units;
using Steamwar.Objects;
using Steamwar.Grid;

namespace Steamwar.Sectors
{
    [Serializable]
    public class SectorBoard
    {
        public BoardCellData[] tileDatas;//{tileId: grass, tileIndex: 0}, {tileId: tree, tileIndex: 1}, ...
        public byte[] tiles;
        public BoardObjects objects;
        public string version;//1.0.0
    }

    [Serializable]
    public class BoardCellData
    {
        public string id;//grass/forest/cliff
        public byte index;//0/1/2
        
    }


}
