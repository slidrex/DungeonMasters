using ServerCore;
using Riptide.Utils;
using DungeonMastersServer.MessageHandlers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DungeonMastersServer
{
    class Program
    {
        private static void RegisterMessageHandlers()
        {

            Register(new PlayerMessageHandler(), new ChatMessageHandler());
        }

        private static NetworkManager _networkManager = null;
        private static bool _isRunning = false;
        private static void Main()
        {





            RiptideLogger.Initialize(Console.WriteLine, false);
            _networkManager = new NetworkManager(7777, 3);
            _isRunning = true;
            RegisterMessageHandlers();




            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            _networkManager.Start();

        }
        private static void Register(params IAbstractMessageHandler[] handlers)
        {
            foreach(var handler in handlers)
            {

                handler.Init();
            }
        }
        private static void MainThread()
        {
            Console.WriteLine($"Main thread started");

            while (_isRunning)
            {
                _networkManager.FixedUpdate();
            }
            _networkManager.Stop();
        }
    }

}
