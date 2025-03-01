using DungeonMastersServer.Repositories;
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
        [MessageHandler((ushort)ClientToServerId.LOBBY_requestSetReady)]
        private static void HandleReady(ushort fromClient, Message message)
        {
            bool isReady = message.GetBool();

            var player = ClientRepository.Service.GetPlayer(fromClient);
            var data = player.GetLobbyData();
            var username = player.Username;
            data.SetReadyStatus(isReady);
            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.LOBBY_responseSetReady);
            msg.AddBool(isReady);
            NetworkManager.Server.Send(message, fromClient);

            var chatMsg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerChatMessage);
            chatMsg.AddString(username);


            var players = ClientRepository.Service.GetPlayers();
            var playerCount = players.Length;
            var readyPlayerCount = players.Count((s) => s.Value.GetLobbyData().IsReady == true);

            chatMsg.AddString((isReady ? $"{username} is ready" : $"{username} is not ready") + $"({readyPlayerCount}/{playerCount})");

            NetworkManager.Server.SendToAll(chatMsg);
        }

    }
}
