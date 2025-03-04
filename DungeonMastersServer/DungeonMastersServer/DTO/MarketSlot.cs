using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.DTO
{
    internal record MarketSlot
    {
        public string ItemName { get; }
        public byte SlotType { get; }
        public bool IsOwned { get; }
        public MarketSlot(Item item, bool isOwned)
        {
            IsOwned = isOwned;
            ItemName = item.Title;
            SlotType = (byte)item.SlotType;
        }
    }
}
