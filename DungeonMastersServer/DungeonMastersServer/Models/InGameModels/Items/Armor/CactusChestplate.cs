using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items.Armor
{
    class CactusChestplate : Item, IArmor
    {
        public int Armor => 25;

        protected override SlotType SlotType => SlotType.Armor;

        internal override ushort Id => 3;

        internal override string Title => "Cactus armor";

        public void OnHit(ushort attackerId)
        {
            //бьем аттакера
            BreakItem();
        }

        internal override string GetDescription()
        {
            return "Does damage to attacker";
        }
    }
}
