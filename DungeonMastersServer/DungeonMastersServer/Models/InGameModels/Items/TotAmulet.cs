using DungeonMastersServer.Models.InGameModels.Items.Interfaces;

namespace DungeonMastersServer.Models.InGameModels.Items
{

    internal sealed class TotAmulet : Item, IRoundDependant
    {
        internal override ushort Id => 5;

        internal override string Title => "Tot's amulet";
        
        public void OnRoundStarted()
        {
            Data.Health += 5;
        }

        internal override string GetDescription()
        {
            return "Healing owner every round by 5 health";
        }
    }
}