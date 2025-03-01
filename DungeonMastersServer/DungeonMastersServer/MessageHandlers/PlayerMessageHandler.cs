using Riptide;
using DungeonMastersServer.Services;

namespace DungeonMastersServer.MessageHandlers
{
    internal class PlayerMessageHandler
    {
        [MessageHandler((ushort)ClientToServerId.sendName)]
        private static void HandleNewPlayerPackage(ushort fromClient, Message message)
        {
            var username = message.GetString();
            PlayerService.Service.AddNewPlayer(fromClient, username);
            
        }
        [MessageHandler((ushort)ClientToServerId.sendMoveInputs)]
        private static void HandlePlayerInputsPackage(ushort fromClient, Message message)
        {
            var player = PlayerService.Service.GetPlayer(fromClient);

            bool w = message.GetBool();
            bool a = message.GetBool();
            bool s = message.GetBool();
            bool d = message.GetBool();

            PlayerMovementService.Service.MovePlayer(fromClient, player, w, a, s, d);
        }
    }
}
