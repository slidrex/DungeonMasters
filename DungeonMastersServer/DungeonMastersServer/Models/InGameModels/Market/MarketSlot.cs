using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DungeonMastersServer.Models.InGameModels.Market
{
    internal record MarketSlot
    {
        public Item Item { get;  }
        public int BuyPrice { get; }
        public int SellPrice { get; }
        public Item? ItemPrice { get; } // Для предметов которые можно улучшать
        public MarketSlot(Item item, int buyPrice, int sellPrice, Item? itemPrice = null)
        {
            Item = item;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            ItemPrice = itemPrice;
        }
    }
}
