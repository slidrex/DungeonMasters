using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using DungeonMastersServer.Services;

namespace DungeonMastersServer.Models.InGameModels.Items.Armor
{

    internal sealed class RaShield : Item, IArmor
    {
        protected override SlotType SlotType => SlotType.Armor;
        public int Armor => 30;

        internal override string Title => "Ra's Shield";


        internal override string GetDescription()
        {
            return "Grants 10 gold every hit on you";
        }

        public void OnBeforeHit(ushort attackerId, DamageType damageType, out float incomingDamageMultiplier)
        {
            incomingDamageMultiplier = 1;
        }

        public void OnAfterHit(ushort attackerId, DamageType damageType)
        {
            Data.AddGold(10);
        }
    }
}