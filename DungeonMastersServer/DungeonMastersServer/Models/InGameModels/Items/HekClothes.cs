using DungeonMastersServer.Models.InGameModels.Items.Interfaces;

namespace DungeonMastersServer.Models.InGameModels.Items
{

    internal sealed class HekClothes : Item, IArmor
    {
        public int Armor => 0;

        internal override ushort Id => 6;
        
        internal override string Title => "Hek Clothes";

        public int Durability = 2;
        public void OnHit(ushort attackerId)
        {
            Durability -= 1;
            if (Durability == 1)
                BreakItem();
        }

        internal override string GetDescription()
        {
            return "Allows clothes owner dodge 2 hits, after this, armor breaks";
        }
    }
}