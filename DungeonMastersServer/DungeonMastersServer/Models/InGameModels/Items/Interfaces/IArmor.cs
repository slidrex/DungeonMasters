using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items.Interfaces
{
    interface IArmor
    {
        int Armor { get; }
        void OnHit(ushort attackerId);
    }
}
