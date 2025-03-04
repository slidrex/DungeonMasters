using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using DungeonMastersServer.Services;

namespace DungeonMastersServer.Models.InGameModels.Items.Magic
{

    internal sealed class TotAmulet : Item, IRoundDependant
    {
        internal override SlotType SlotType => SlotType.Magic;

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