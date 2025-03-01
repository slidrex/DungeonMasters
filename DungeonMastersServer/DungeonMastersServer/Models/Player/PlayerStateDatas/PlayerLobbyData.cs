using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.Player.PlayerDatas
{
    class PlayerLobbyData : PlayerStateData
    {
        public float LobbySpeed = 0.04f;
        public bool IsReady { get; private set; }
        public void SetReadyStatus(bool isReady)
        {
            IsReady = isReady;
        }
    }
}
