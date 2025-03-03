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
                new MarketSlot(new MagicApple(),buyPrice: 15, sellPrice: 5),
                new MarketSlot(new GoldenScarab(), buyPrice: 20, sellPrice: 10),
                new MarketSlot(new TotAmulet(), buyPrice: 15, sellPrice: 5),
                new MarketSlot(new SetSceptre(), buyPrice: 100, sellPrice: 40),
                new MarketSlot(new HekClothes(), buyPrice: 25, sellPrice: 15),
                new MarketSlot(new CactusChestplate(), buyPrice: 10, sellPrice: 5),
                new MarketSlot(new RaShield(), buyPrice: 40, sellPrice: 20),
                new MarketSlot(new SealAnkh(), buyPrice: 25, sellPrice: 15),
                new MarketSlot(new PharaohHeart(), buyPrice: 30, sellPrice: 15),
                new MarketSlot(new SetDagger(), buyPrice: 20, sellPrice: 10),
                new MarketSlot(new SetSceptre(), buyPrice: 25, sellPrice: 10),
                new MarketSlot(new WoodenSword(), buyPrice: 15, sellPrice: 5),
                new MarketSlot(new OsirisSceptre(), buyPrice: 20, sellPrice: 10)
                ];
        }
        public override void Init()
        {
            base.Init();
            RegisterSlots();
        }

        public MarketSlot[] GetMarketSlots()
        {
            return RegisterSlots();
        }
    }
}
