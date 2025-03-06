using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using DungeonMastersServer.Services;
using System.Reflection.Metadata.Ecma335;

namespace DungeonMastersServer.Models.InGameModels.Items.Armor
{

    internal sealed class HekClothes : ArmorItem
    {
        public override int Armor => 0;
        internal override string Title => "Hek Clothes";

        public int Durability = 2;
        public override ItemStat[] GetAdditionalStats()
        {
            return [ ItemStat.Create("Durability", Durability)];
        }
        internal override string GetDescription() => "Allows clothes owner dodge 2 hits, then it breaks";
        public override void OnAfterHit(ushort attackerId, DamageType damageType)
        {
            // Dodge
            Durability -= 1;
            if (Durability == 1)
                BreakItem();
        }
    }
}