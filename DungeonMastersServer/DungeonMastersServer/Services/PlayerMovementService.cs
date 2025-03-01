using DungeonMastersServer.MessageHandlers;
using Riptide;
using System.Numerics;
using ServerCore.Utils;
using DungeonMastersServer.Repositories;
using DungeonMastersServer.Models.Player;

namespace DungeonMastersServer.Services
{
    class PlayerMovementService : SingletonService<PlayerMovementService>
    {
        public void MovePlayer(ushort fromClient, PlayerClient player, bool w, bool a, bool s, bool d)
        {
            Vector2 position = player.Position;
            
            
            
            float deltaSpeed = 0.04f;
            
            
            Vector2 movement = Vector2.Zero;



            if (w && position.Y < 5)
            {
                movement.Y += 1;
            }
            if (a && position.X > -5)
            {
                movement.X -= 1;
            }
            if (s && position.Y > -5)
            {
                movement.Y -= 1;
            }
            if (d && position.X < 5)
            {
                movement.X += 1;
            }


            if (movement != Vector2.Zero)
            {
                movement = Vector2.Normalize(movement);
            }

            
            position.X += movement.X * deltaSpeed;
            position.Y += movement.Y * deltaSpeed;
            player.Position = position;
            var msg = Message.Create(MessageSendMode.Unreliable, (ushort)ServerToClientId.playerMovement);
            msg.AddUShort(fromClient);
            msg.AddVector2(position);
            ClientRepository.Service.SetPlayer(fromClient, player);
            NetworkManager.Server.SendToAll(msg);
        }
    }
}
