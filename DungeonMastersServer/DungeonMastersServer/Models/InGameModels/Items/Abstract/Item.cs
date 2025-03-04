using DungeonMastersServer.Models.Player.PlayerDatas;

namespace DungeonMastersServer.Models.InGameModels.Items.Abstract
{
    internal abstract class Item
    {
        internal abstract SlotType SlotType { get; }
        protected PlayerGameData Data { get; set; }
        internal abstract string Title { get;  }
        internal abstract string GetDescription();
        protected void BreakItem()
        {

        }

    }
    enum SlotType
    {
        Magic = 0,
        Armor = 1,
        Weapon = 2,
        Artifact = 3
    }
}
