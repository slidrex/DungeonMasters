using DungeonMastersServer.Models.Player.PlayerDatas;
using DungeonMastersServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items.Abstract
{
    internal abstract class Item
    {
        protected abstract SlotType SlotType { get; }
        protected PlayerGameData Data { get; set; }
        internal abstract string Title { get;  }
        internal abstract string GetDescription();
        protected void BreakItem()
        {

        }

    }
    enum SlotType
    {
        Magic,
        Armor,
        Weapon,
        Artifact
    }
}
