using ServerCore.Utils;
using Riptide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer
{

    
    public sealed class NetworkManager
    {


        
        public static Server Server { get; private set; }
        private ushort _port;
        private ushort _maxClients;

        public NetworkManager(ushort port, ushort maxClients)
        {
            Server = new Server();

            _port = port;
            _maxClients = maxClients;
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
