using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.Player.PlayerDatas
{

    enum PlayerLifeState
    {
        Alive = 0,
        Dead = 1,
    }

    enum PlayerDebuffState
    {
        
    }

    enum PlayerBuffState
    {
        
    }
    class PlayerGameData : PlayerStateData
    {
        public float DefaultSpeed = 0.04f;
        public int Health { get; set; }
        
        public int MaxHealth { get; set; } 
        
        public PlayerLifeState LifeState { get; private set; }

        public override void EnterState()
        {
            MaxHealth = 100;
            Health = MaxHealth;
            LifeState = PlayerLifeState.Alive;
        }

        public void KillPlayer()
        {
            LifeState = PlayerLifeState.Dead;
        }
    }
}
