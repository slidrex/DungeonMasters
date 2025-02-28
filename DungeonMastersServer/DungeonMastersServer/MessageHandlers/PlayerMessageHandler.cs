using DungeonMastersServer.Models;
using DungeonMastersServer.Utils;
using Riptide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.MessageHandlers
{
    class PlayerMessageHandler
    {
        public static Dictionary<ushort, Player> Players = new Dictionary<ushort, Player>();


        [MessageHandler((ushort)ClientToServerId.sendName)]
        private static void Name(ushort fromClient, Message message)
        {
            var username = message.GetString();

            if (Players != null)
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

            Players.Add(fromClient, new Player(username));
            Console.WriteLine(username + " connected the game!");
            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerConnected);
            msg.AddUShort(fromClient);
            msg.AddString(username);
            NetworkManager.Server.SendToAll(msg);
        }
        [MessageHandler((ushort)ClientToServerId.sendMoveInputs)]
        private static void SendInputs(ushort fromClient, Message message)
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
    }
}
