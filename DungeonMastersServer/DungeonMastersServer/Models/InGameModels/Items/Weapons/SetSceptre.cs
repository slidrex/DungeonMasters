using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;

namespace DungeonMastersServer.Models.InGameModels.Items.Weapons
{

    internal sealed class SetSceptre : Item, IRoundDependant
    {
        protected override SlotType SlotType => SlotType.Weapon;

        internal override string Title => "Sceptre of Set's";
        
        public void OnRoundStarted()
        {
            Data.Health -= 15;
            Data.AddGold(5);
        }

        internal override string GetDescription()
        {
            return "Grants you 5 gold every round, but takes for this 15 health every round";
        }
    }
}