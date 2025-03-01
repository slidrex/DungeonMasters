using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.Player
{
    public abstract class PlayerStateData
    {
        protected PlayerClient Player;
        
        internal void AttachPlayer(PlayerClient player)
        {
            Player = player;
        }
        
        public virtual void EnterState() { }
        public virtual void ExitState() { }
    }
}
