using DungeonMastersServer.Models.InGameModels.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Market
{
    internal record MarketSlot
    {
        public Item Item { get; set; }
        public int MoneyPrice { get; set; }
        public Item ItemPrice { get; set; } // Для предметов которые можно улучшать
    }
}
