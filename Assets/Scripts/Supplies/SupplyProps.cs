﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Steamwar.Supplies
{
    [Serializable]
    public class SupplyProps
    {
        [Range(0, 8192)]
        public int moneyAmount = 0;

        public int this[string name]
        {
            get
            {
                return name switch
                {
                    "money" => moneyAmount,
                    _ => moneyAmount,
                };
            }
        }
    }
}
