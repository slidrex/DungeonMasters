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
    internal sealed class WoodenSword : Item, IMountable
    {
        protected override SlotType SlotType => SlotType.Weapon;
        
        private int _damage = 10;

        internal override string Title => "Wooden sword";

        public void OnMount()
        {
            Data.AddDamageUnit(_damage);
        }

        public void OnUnmount()
        {
            Data.AddDamageUnit(-_damage);
        }

        internal override string GetDescription()
        {
            return $"Наносит {_damage} урона";
        }
    }
}
