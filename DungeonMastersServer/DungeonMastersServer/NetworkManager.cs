using DungeonMastersServer.Models;
using Riptide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer
{

    enum ServerToClientId
    {
        playerConnected = 0,
        playerDisconnected = 1,
        playerMovement = 2
    }
    enum ClientToServerId
    {
        sendName = 0,
        sendMoveInputs = 1
    }
    class NetworkManager
    {
        public static Dictionary<ushort, Player> Players = new Dictionary<ushort, Player>();


        
        public static Server Server { get; private set; }
        private ushort _port;
        private ushort _maxClients;

        public NetworkManager(ushort port, ushort maxClients)
        {
            Server = new Server();
            Server.ClientDisconnected += Server_ClientDisconnected;

            _port = port;
            _maxClients = maxClients;
        }
        
        private void Server_ClientDisconnected(object? sender, ServerDisconnectedEventArgs e)
        {
            var senderId = e.Client.Id;
            Console.WriteLine(Players[senderId].Username + " left the game!");
            Players.Remove(senderId);

            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerDisconnected);
            msg.AddUShort(senderId);
            Server.SendToAll(msg);
        }

        public void Start()
        {
            Server.Start(_port, _maxClients);
        }
        public void FixedUpdate()
        {
            Server.Update();
        }
        public void Stop()
        {
            Server.Stop();
        }
        [MessageHandler((ushort)ClientToServerId.sendName)]
        private static void Name(ushort fromClient, Message message)
        {
            var username = message.GetString();

            if(Players != null)
            {
                var playersArray = Players.ToArray();

                foreach (var player in playersArray)
                {
                    var existingPlayer = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerConnected);
                    existingPlayer.AddUShort(player.Key);
                    Console.WriteLine(player.Value.Username);
                    existingPlayer.AddString(player.Value.Username);
                    Server.Send(existingPlayer, fromClient);
                }
            }

            Players.Add(fromClient, new Player(username));
            Console.WriteLine(username + " connected the game!");
            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerConnected);
            msg.AddUShort(fromClient);
            msg.AddString(username);
            Server.SendToAll(msg);
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
