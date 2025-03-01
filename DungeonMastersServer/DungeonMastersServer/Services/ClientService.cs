using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Models;
using DungeonMastersServer.Models.Player.PlayerDatas;
using DungeonMastersServer.Repositories;
using Riptide;
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
            


            var existingPlayers = ClientRepository.Service.GetPlayers();
            if (existingPlayers != null && existingPlayers.Length > 0)
            {
                SpawnExistingPlayers(fromClient);
            }
            var player = new Models.Player.PlayerClient(username, new PlayerLobbyData());
            ClientRepository.Service.AddPlayer(fromClient, player);

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
            NetworkManager.Server.SendToAll(msg);
        }
    }
}
