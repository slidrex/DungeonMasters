using DungeonMastersServer.Models.InGameModels.Items.Interfaces;

namespace DungeonMastersServer.Models.InGameModels.Items
{

    internal sealed class RaShield : Item, IArmor
    {
        public int Armor => 30;

        internal override ushort Id => 8;

        internal override string Title => "Ra's Shield";

        public void OnHit(ushort attackerId)
        {
            Data.AddGold(10);
        }

        internal override string GetDescription()
        {
            return "Grants 10 gold every hit on you";
        }
    }
}