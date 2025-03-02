using DungeonMastersServer.Models.Player.PlayerDatas;
using DungeonMastersServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.Player
{
    public class PlayerClient
    {
        public readonly string Username;

        public bool Freezed { get; private set; }
        public Vector2 Position { get; set; }
        
        private PlayerStateData _stateData { get; set; }
        public PlayerClient(string username, PlayerStateData stateData)
        {
            _stateData = stateData;
            Username = username;
            _stateData.AttachPlayer(this);
            Freezed = false;
        }


        public void SetPlayerStateData(PlayerStateData stateData)
        {
            _stateData = stateData;
        }

        public void SetFreeze(bool freeze)
        {
            Freezed = freeze;
        }
        internal float GetSpeed()
        {
            var gameData = (_stateData as PlayerGameData);
            return gameData == null ? ((PlayerLobbyData)_stateData).LobbySpeed : gameData.DefaultSpeed;
        }
        internal PlayerGameData GetGameData()
        {
            var data = _stateData as PlayerGameData;
            if(data == null)
            {
                throw new Exception("Trying to get Player Game Data but it has another state or empty");
            }
            return data;
            
        }
        internal PlayerLobbyData GetLobbyData()
        {
            var data = _stateData as PlayerLobbyData;
            if (data == null)
            {
                throw new Exception("Trying to get Player Lobby Data but it has another state or empty");
            }
            return data;
        }
        internal void ChangeState(PlayerStateData state)
        {
            _stateData.ExitState();
            _stateData = state;
            _stateData.EnterState();
        }
    }
}
