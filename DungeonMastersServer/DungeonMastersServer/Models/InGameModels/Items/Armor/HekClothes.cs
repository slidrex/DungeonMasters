using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using DungeonMastersServer.Services;

namespace DungeonMastersServer.Models.InGameModels.Items.Armor
{

    internal sealed class HekClothes : Item, IArmor
    {
        internal override SlotType SlotType => SlotType.Armor;
        public int Armor => 0;

        
        internal override string Title => "Hek Clothes";

        public int Durability = 2;
        public void OnHit(ushort attackerId, DamageType damageType)
        {
            
        }

        internal override string GetDescription()
        {
            return "Allows clothes owner dodge 2 hits, then it breaks";
        }

        public void OnBeforeHit(ushort attackerId, DamageType damageType, out float incomingDamageMultiplier)
        {
            incomingDamageMultiplier = 1;
        }

        public void OnAfterHit(ushort attackerId, DamageType damageType)
        {
            Durability -= 1;
            if (Durability == 1)
                BreakItem();
        }
    }
}