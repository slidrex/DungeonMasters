using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonMastersServer.Repositories;
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
            var damage = 10;
            
            GameService.Service.HitRequest(hitTarget, fromClient, damage);
        }

        [MessageHandler((ushort)ClientToServerId.GAME_REQUEST_END_TURN)]
        public static void HandleEndTurnRequestPackage(ushort fromClient, Message message)
        {
            GameService.Service.PlayerPressedReady(fromClient);
        }
    }

