using DungeonMastersServer.Models.InGameModels.Items.Interfaces;

namespace DungeonMastersServer.Models.InGameModels.Items
{

    internal sealed class SetSceptre : Item, IRoundDependant
    {
        internal override ushort Id => 7;

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