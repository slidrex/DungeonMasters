using Riptide;
using DungeonMastersServer.Services;
using DungeonMastersServer.Repositories;

namespace DungeonMastersServer.MessageHandlers
{
    internal class PlayerMoveMessageHandler
    {
        
        [MessageHandler((ushort)ClientToServerId.sendMoveInputs)]
        private static void HandlePlayerInputsPackage(ushort fromClient, Message message)
        {
            var player = ClientRepository.Service.GetPlayer(fromClient);

            bool w = message.GetBool();
            bool a = message.GetBool();
            bool s = message.GetBool();
            bool d = message.GetBool();

            PlayerMovementService.Service.MovePlayer(fromClient, player, w, a, s, d);
        }
    }
}
