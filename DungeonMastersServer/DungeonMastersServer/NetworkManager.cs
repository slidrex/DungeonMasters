using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Models;
using DungeonMastersServer.Utils;
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
            Console.WriteLine(PlayerMessageHandler.Players[senderId].Username + " left the game!");
            PlayerMessageHandler.Players.Remove(senderId);

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
        
    }
}
