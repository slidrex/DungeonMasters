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

        private int _durability = 2;
        internal override string GetDescription() => "Allows clothes owner dodge 2 hits, then it breaks";
        public override void OnAfterHit(ushort attackerId, DamageType damageType)
        {
            // Dodge
            _durability -= 1;
            if (_durability == 1)
                BreakItem();
        }
    }
}