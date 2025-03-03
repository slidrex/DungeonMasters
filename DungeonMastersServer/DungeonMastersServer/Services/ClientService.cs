using System.Numerics;
using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Models;
using DungeonMastersServer.Models.Player;
using DungeonMastersServer.Models.Player.PlayerDatas;
using DungeonMastersServer.Repositories;
using Riptide;
using ServerCore.Utils;

namespace DungeonMastersServer.Services
{
    class ClientService : SingletonService<ClientService>
    {
        
        public ClientService()
        {
            NetworkManager.Server.ClientDisconnected += Server_ClientDisconnected;
        }
        ~ClientService(){
            NetworkManager.Server.ClientDisconnected -= Server_ClientDisconnected;
        }
        
        public void AddNewPlayer(ushort fromClient, string username)
        {
            if (StateManagerService.Service.CurrentState != GameState.InLobby)
            {
                NetworkManager.Server.DisconnectClient(fromClient);
            }
            Console.WriteLine(username + " connected the game!");
            var player = new Models.Player.PlayerClient(username, new PlayerLobbyData());
            ClientRepository.Service.AddPlayer(fromClient, player);
        }
        internal bool CheckAllPlayersLoadGame()
        {
            var players = ClientRepository.Service.GetPlayers();
            bool isAllPlayersLoad = true;
            for(int i = 0; i < players.Length; i++)
            {
                var playerLobbyData = players[i].Value.GetLobbyData();
                if (!playerLobbyData.IsLoadedToGameScene)
                {
                    isAllPlayersLoad = false;
                    break;
                }
             }
            return isAllPlayersLoad;
        }
        internal void SpawnNewPlayer(ushort fromClient, string username, bool includeExisting = true)
        {
            var existingPlayers = ClientRepository.Service.GetPlayers();
            if (existingPlayers != null && existingPlayers.Length > 0 && includeExisting)
            {
                SpawnExistingPlayers(fromClient);
            }
            
            
            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.spawnPlayer);
            msg.AddUShort(fromClient);
            msg.AddString(username);
            NetworkManager.Server.SendToAll(msg);
        }
        
        private void SpawnExistingPlayers(ushort fromClient)
        {
            var playersArray = ClientRepository.Service.GetPlayers();

            foreach (var player in playersArray)
            {
                if(fromClient == player.Key) continue;
                
                var existingPlayer = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.spawnPlayer);
                existingPlayer.AddUShort(player.Key);
                existingPlayer.AddString(player.Value.Username);
                NetworkManager.Server.Send(existingPlayer, fromClient);
            }
        }

        private void Server_ClientDisconnected(object? sender, ServerDisconnectedEventArgs e)
        {
            var senderId = e.Client.Id;
            Console.WriteLine(ClientRepository.Service.GetPlayer(senderId).Username + " left the game!");
            
            ClientRepository.Service.RemovePlayer(senderId);

            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerDisconnected);
            msg.AddUShort(senderId);
            
            var players = ClientRepository.Service.GetPlayers();

            if (players.Length == 0)
                RestoreServer();
            
            NetworkManager.Server.SendToAll(msg);
        }

        private void RestoreServer()
        {
            if (StateManagerService.Service.CurrentState != GameState.InLobby)
                StateManagerService.Service.SetState(GameState.InLobby);
            
            ClientRepository.Service.ClearPlayers();
            Console.WriteLine("Server is empty, reloaded server");
        }
        public void TransportPlayer(ushort fromClient, Vector2 position)
        {
            var player = ClientRepository.Service.GetPlayer(fromClient);

            player.Position = position;
            
            ClientRepository.Service.SetPlayer(fromClient, player);

            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerTeleport);
            msg.AddUShort(fromClient);
            msg.AddVector2(player.Position);
            NetworkManager.Server.SendToAll(msg);
        }

        public void TransportAllPlayers(int freezeDuration)
        {
            var players = ClientRepository.Service.GetPlayers();
            
            Vector2[] positions = new Vector2[]
            {
                new Vector2(-10, 9),
                new Vector2(10, 9),
                new Vector2(-10, -8),
                new Vector2(10, -8),
            };

            var i = 0;
            foreach (var (index, player) in players)
            {
                _ = FreezePlayer(index, freezeDuration);
                TransportPlayer(index, positions[i]);
                i++;
            }

        }

        public async Task FreezePlayer(ushort fromClient, int freezeDuration)
        {
            var player = ClientRepository.Service.GetPlayer(fromClient);
            
            player.SetFreeze(true);

            await Task.Delay(freezeDuration);
            
            player.SetFreeze(false);
        }
    }
}
