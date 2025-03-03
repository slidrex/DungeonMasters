using Riptide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.MessageHandlers
{
    class MarketMessageHandler
    {
        [MessageHandler((ushort)ClientToServerId.GAME_REQUESTHIT)]
        public static void BuyItemRequestPackage(ushort fromClient, Message message)
        {

        }
        [MessageHandler((ushort)ClientToServerId.GAME_REQUESTHIT)]
        public static void SellItemRequestPackage(ushort fromClient, Message message)
        {

        }
    }
}
