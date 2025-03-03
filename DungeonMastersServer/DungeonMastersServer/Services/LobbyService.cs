using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Repositories;
using Riptide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Services
{
    class LobbyService : SingletonService<LobbyService>
    {
        public void HandleReady(ushort fromClient, bool isReady)
        {
            var player = ClientRepository.Service.GetPlayer(fromClient);
            var data = player.GetLobbyData();
            var username = player.Username;
            data.SetReadyStatus(isReady);
            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.LOBBY_responseSetReady);
            msg.AddBool(isReady);
            NetworkManager.Server.Send(msg, fromClient);


            OnReadyChanged(username, isReady);

            
        }
        public void OnReadyChanged(string lastReadyUsername, bool isReady)
        {
            var players = ClientRepository.Service.GetPlayers();
            var playerCount = players.Length;
            var readyPlayerCount = players.Count((s) => s.Value.GetLobbyData().IsReady == true);

            ChatMessageService.Service.SendSystemChatMessage((isReady ? $"{ lastReadyUsername} is ready" : $"{lastReadyUsername} is not ready") + $"({readyPlayerCount}/{playerCount})");

            if(readyPlayerCount == playerCount)
            {
                var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.LOBBY_SWITCH_TO_GAME_SCENE);
                NetworkManager.Server.SendToAll(msg);
            }

        }
    }
}

/*
                if(readyPlayerCount < 3)
                {
                    StateManagerService.Service.SetState(GameState.InGame);
                    
                    ChatMessageService.Service.SendSystemChatMessage("Can't start game. Need minimum 3 players");
                }
                else
                {
                    StateManagerService.Service.SetState(GameState.InGame);
                    ClientService.Service.TransportAllPlayers();
                }
*/