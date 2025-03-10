﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.Player
{
    public abstract class PlayerStateData
    {
        internal PlayerClient PlayerClient;
        
        internal void AttachPlayer(PlayerClient player)
        {
            PlayerClient = player;
        }
        
        public virtual void EnterState() { }
        public virtual void ExitState() { }
    }
}
