using DungeonMastersServer.Services;
using Riptide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.MessageHandlers
{
    internal class LobbyMessageHandler
    {
        [MessageHandler((ushort)ClientToServerId.sendName)]
        private static void HandleNewPlayerPackage(ushort fromClient, Message message)
        {
            var username = message.GetString();
            ClientService.Service.AddNewPlayer(fromClient, username);
        }
        

    }
}
