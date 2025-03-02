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

            var player = new Models.Player.PlayerClient(username, new PlayerLobbyData());
            ClientRepository.Service.AddPlayer(fromClient, player);
        }

        internal void SpawnNewPlayer(ushort fromClient, string username)
        {
            var existingPlayers = ClientRepository.Service.GetPlayers();
            if (existingPlayers != null && existingPlayers.Length > 0)
            {
                SpawnExistingPlayers(fromClient);
            }
            
            Console.WriteLine(username + " connected the game!");
            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerConnected);
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
                
                var existingPlayer = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerConnected);
                existingPlayer.AddUShort(player.Key);
                Console.WriteLine(player.Value.Username);
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

        public void TransportAllPlayers()
        {
            var players = ClientRepository.Service.GetPlayers();
            
            Vector2[] positions = new Vector2[]
            {
                new Vector2(-10, 9),
                new Vector2(10, 9),
                new Vector2(-10, -8),
                new Vector2(10, -8),
            };
            
            foreach (var (player, index) in players.Select((p, i) => (p, i)))
            {
                _ = FreezePlayer(player.Key);
                TransportPlayer(player.Key, positions[index]);
            }

        }

        public async Task FreezePlayer(ushort fromClient)
        {
            var player = ClientRepository.Service.GetPlayer(fromClient);
            
            player.SetFreeze(true);

            await Task.Delay(5000);
            
            player.SetFreeze(false);
        }
    }
}
