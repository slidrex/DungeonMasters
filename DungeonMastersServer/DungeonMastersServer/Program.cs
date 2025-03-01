using ServerCore;
using Riptide.Utils;
using DungeonMastersServer.Services;

namespace DungeonMastersServer
{
    class Program
    {
        private static void RegisterServices()
        {

            Register(new PlayerService(), new PlayerMovementService());
        }

        private static NetworkManager _networkManager = null;
        private static bool _isRunning = false;
        private static void Main()
        {
            RiptideLogger.Initialize(Console.WriteLine, false);
            _networkManager = new NetworkManager(7777, 3);
            _isRunning = true;
            RegisterServices();


            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            _networkManager.Start();

        }
        private static void Register(params IInitable[] handlers)
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
