using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Models.InGameModels.Items;
using DungeonMastersServer.Models.InGameModels.Items.Armor;
using DungeonMastersServer.Models.InGameModels.Items.Magic;
using DungeonMastersServer.Models.InGameModels.Items.Weapons;
using DungeonMastersServer.Models.InGameModels.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Repositories
{
    class MarketRepository : SingletonService<MarketRepository>
    {

        //в магазине продаваться будут
        private MarketSlot[] RegisterSlots()
        {
            return [
                new MarketSlot(new MagicApple(),buyPrice: 10, sellPrice: 5),
                new MarketSlot(new GoldenScarab(), buyPrice: 50, sellPrice: 20),
                new MarketSlot(new TotAmulet(), buyPrice: 50, sellPrice: 20),
                new MarketSlot(new SetSceptre(), buyPrice: 100, sellPrice: 40),
                new MarketSlot(new HekClothes(), buyPrice: 120, sellPrice: 40)
                ];
        }
        public override void Init()
        {
            base.Init();
            RegisterSlots();
        }
    }
}
