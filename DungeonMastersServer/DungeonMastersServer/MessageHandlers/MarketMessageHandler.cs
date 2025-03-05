using Riptide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonMastersServer.Services;

namespace DungeonMastersServer.MessageHandlers
{
    class MarketMessageHandler
    {
        [MessageHandler((ushort)ClientToServerId.REQUEST_ITEM_STATS)]
        private static void RequestItemStatsPackage(ushort fromClient, Message message)
        {
            string itemName = message.GetString();
            MarketService.Service.SendItemStats(fromClient, itemName);
        }

        [MessageHandler((ushort)ClientToServerId.REQUEST_PLAYER_ITEMS)]
        private static void RequestPlayerItemsPackage(ushort fromClient, Message message)
        {
            MarketService.Service.SendPlayerItems(fromClient);
        }
    }
}
