using System.Net.Sockets;
using System.Net;
using Riptide;
using System.Text;
using Riptide.Utils;

namespace DungeonMastersServer
{
    class Program
    {
        private static NetworkManager _networkManager = null;
        private static bool _isRunning = false;
        private static void Main()
        {
            RiptideLogger.Initialize(Console.WriteLine, false);
            _networkManager = new NetworkManager(7777, 3);
            _isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            _networkManager.Start();

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
