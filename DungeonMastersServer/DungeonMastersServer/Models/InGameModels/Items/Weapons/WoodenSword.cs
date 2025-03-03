using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using DungeonMastersServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items.Weapons
{
    internal sealed class WoodenSword : Item, IHitItem
    {
        protected override SlotType SlotType => SlotType.Weapon;
        public int Damage => 15;


        internal override string Title => "Wooden sword";

        public void OnHit(ushort targetId)
        {
            
        }

        internal override string GetDescription()
        {
            return $"Наносит {Damage} урона";
        }
    }
}
