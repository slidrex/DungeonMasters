using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using DungeonMastersServer.Services;
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

        internal override SlotType SlotType => SlotType.Armor;


        internal override string Title => "Cactus armor";

        public void OnAfterHit(ushort attackerId, DamageType damageType)
        {
            BreakItem();
        }

        public void OnBeforeHit(ushort attackerId, DamageType damageType, out float incomingDamageMultiplier)
        {
            incomingDamageMultiplier = 1;
        }


        internal override string GetDescription()
        {
            return "Reflects incoming damage to attacker. Only once";
        }
    }
}
