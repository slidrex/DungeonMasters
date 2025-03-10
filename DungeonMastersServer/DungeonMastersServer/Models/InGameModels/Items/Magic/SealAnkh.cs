﻿using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using DungeonMastersServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items.Magic
{
    //Passive item
    class SealAnkh : Item, IIncomingDamageMultiplier
    {
        internal override SlotType SlotType => SlotType.Magic;


        internal override string Title => "The seal of Ankh";

        public float GetIncomingDamageMultiplier(ushort attackerId, DamageType damageType)
        {
            if(damageType == DamageType.Magical)
            {
                BreakItem();
                return 0;
            }
            return 1;
        }

        internal override string GetDescription()
        {
            return "Blocking all magic damage, but then breaks";
        }
    }
}
