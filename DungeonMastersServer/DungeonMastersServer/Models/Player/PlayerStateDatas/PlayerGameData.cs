using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.Player.PlayerDatas
{
    class PlayerGameData : PlayerStateData
    {
        public float DefaultSpeed = 0.04f;
        public int Health { get; set; }
        
        public int MaxHealth { get; set; } 

        public override void EnterState()
        {
            Health = 100;
            MaxHealth = 100;
        }
    }
}
