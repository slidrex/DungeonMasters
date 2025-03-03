﻿using DungeonMastersServer.Models.InGameModels.Items.Interfaces;
using DungeonMastersServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items
{
    internal sealed class WoodenSword : Item, IHitItem
    {
        public int Damage => 15;

        internal override ushort Id => 2;

        internal override string Title => "Wooden sword";

        public void OnHit(ushort targetId)
        {
            
        }

        internal override string GetDescription()
        {
            return "Does damage";
        }
    }
}
