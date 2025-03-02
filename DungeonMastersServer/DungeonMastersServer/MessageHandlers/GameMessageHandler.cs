using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonMastersServer.Services;
using Riptide;

namespace DungeonMastersServer.MessageHandlers;

    public class GameMessageHandler
    {
        [MessageHandler((ushort)ClientToServerId.GAME_REQUESTHIT)]
        public static void HandleHitRequestPackage(ushort fromClient, Message message)
        {
            if (StateManagerService.Service.CurrentState != GameState.InGame)
                return;
            
            var hitTarget = message.GetUShort();
            var damage = message.GetInt();
            
            GameService.Service.HitRequest(hitTarget, fromClient, damage);
        }
    }

