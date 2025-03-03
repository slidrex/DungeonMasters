using DungeonMastersServer.Models.Player.PlayerDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items
{
    internal abstract class Item
    {
        protected PlayerGameData Data { get; set; }
        internal abstract ushort Id { get; }
        internal abstract string Title { get;  }
        internal abstract string GetDescription();
        protected void BreakItem()
        {

        }
    }
}
