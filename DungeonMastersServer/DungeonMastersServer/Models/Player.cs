using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models
{
    public class Player
    {
        public readonly string Username;
        public Vector2 Position { get; set; }
        public Player(string username)
        {
            Username = username;
        }
    }
}
