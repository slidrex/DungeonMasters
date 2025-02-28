using DungeonMastersServer.Models;
using ServerCore.Utils;
using Riptide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.MessageHandlers
{
    internal class PlayerMessageHandler : MessageHandler<PlayerMessageHandler>
    {

        public static Dictionary<ushort, Player> Players = new Dictionary<ushort, Player>();
        public PlayerMessageHandler()
        {
            NetworkManager.Server.ClientDisconnected += Server_ClientDisconnected;
        }

        [MessageHandler((ushort)ClientToServerId.sendName)]
        private static void HandleNewPlayerPackage(ushort fromClient, Message message)
        {
            Singleton.HandleNewPlayer(fromClient, message);
        }
        [MessageHandler((ushort)ClientToServerId.sendMoveInputs)]
        private static void HandlePlayerInputsPackage(ushort fromClient, Message message)
        {
            Singleton.HandlePlayerInputs(fromClient, message);
        }
        public void HandlePlayerInputs(ushort fromClient, Message message)
        {
            var player = Players[fromClient];
            bool w = message.GetBool();
            bool a = message.GetBool();
            bool s = message.GetBool();
            bool d = message.GetBool();

            Vector2 position = player.Position;

            if (w)
            {
                position.Y += 0.03f;
            }
            if (a)
            {
                position.X -= 0.03f;
            }
            if (s)
            {
                position.Y -= 0.03f;
            }
            if (d)
            {
                position.X += 0.03f;
            }
            player.Position = position;
            var msg = Message.Create(MessageSendMode.Unreliable, (ushort)ServerToClientId.playerMovement);
            msg.AddUShort(fromClient);
            msg.AddVector2(position);
            Players[fromClient] = player;
            NetworkManager.Server.SendToAll(msg);
        }
        public void HandleNewPlayer(ushort fromClient, Message message)
        {
            var username = message.GetString();

            if (Players != null)
            {
                SpawnExistingPlayers(fromClient);
            }

            Players.Add(fromClient, new Player(username));
            Console.WriteLine(username + " connected the game!");
            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerConnected);
            msg.AddUShort(fromClient);
            msg.AddString(username);
            NetworkManager.Server.SendToAll(msg);
        }

        

        private static void SpawnExistingPlayers(ushort fromClient)
        {
            var playersArray = Players.ToArray();

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
            Console.WriteLine(PlayerMessageHandler.Players[senderId].Username + " left the game!");
            PlayerMessageHandler.Players.Remove(senderId);

            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerDisconnected);
            msg.AddUShort(senderId);
            NetworkManager.Server.SendToAll(msg);
        }


    }
}
