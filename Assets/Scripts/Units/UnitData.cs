﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steamwar.Factions;
using Steamwar.Objects;

namespace Steamwar.Units
{
    public class UnitData : ObjectData<UnitType, UnitDataSerializable>
    {
        public float health;
        public float movment;

        public override ObjectKind GetKind()
        {
            return ObjectKind.UNIT;
        }

        public override void WriteData(UnitDataSerializable serializable)
        {
            base.WriteData(serializable);
            serializable.health = health;
            serializable.movment = movment;
        }

        public override void ReadData(UnitDataSerializable serializable)
        {
            base.ReadData(serializable);
            health = serializable.health;
            movment = serializable.movment;
        }

        public virtual UnitData Copy()
        {
            UnitData data = new UnitData();
            data.ReadData(WriteData());
            return data;
        }
    }

    [Serializable]
    public class UnitDataSerializable : ObjectDataSerializable
    {
        public float health;
        public float movment;
    }
}
