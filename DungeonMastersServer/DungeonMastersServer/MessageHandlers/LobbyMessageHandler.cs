using DungeonMastersServer.Repositories;
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

        [MessageHandler((ushort)ClientToServerId.LOBBY_LOAD_PLAYERS)]
        private static void HandleLoadLobbyScene(ushort fromClient, Message message)
        {
            ClientService.Service.SpawnNewPlayer(fromClient, message.GetString());
        }

        [MessageHandler((ushort)ClientToServerId.LOBBY_GAME_SCENE_LOADED)]
        private static void ClientGameSceneLoaded(ushort fromClient, Message message)
        {
            var client = ClientRepository.Service.GetPlayer(fromClient).GetLobbyData();
            client.SetAsLoad();
            ClientService.Service.SpawnNewPlayer(fromClient, client.Player.Username, false);

            if (ClientService.Service.CheckAllPlayersLoadGame())
            {
                ChatMessageService.Service.SendSystemChatMessage("Game starts in " + 5 + " seconds");
                _ = GameService.Service.SetTimer(5, () =>
                {
                    StateManagerService.Service.SetState(GameState.InGame);
                    ChatMessageService.Service.SendSystemChatMessage("Game started!");
                });
                
            }
            
        }
    }
}
