using DungeonMastersServer.Services;
using Riptide;

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
        [MessageHandler((ushort)ClientToServerId.LOBBY_requestSetReady)]
        private static void HandleReady(ushort fromClient, Message message)
        {
            bool isReady = message.GetBool();

            LobbyService.Service.HandleReady(fromClient, isReady);
        }

        [MessageHandler((ushort)ClientToServerId.LOBBY_loadLobbyScene)]
        private static void HandleLoadLobbyScene(ushort fromClient, Message message)
        {
            ClientService.Service.SpawnNewPlayer(fromClient, message.GetString());
        }
    }
}
