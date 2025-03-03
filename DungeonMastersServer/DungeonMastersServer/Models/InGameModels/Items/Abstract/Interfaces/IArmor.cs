using DungeonMastersServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces
{
    interface IArmor
    {

        int Armor { get; }
        void OnBeforeHit(ushort attackerId, DamageType damageType, out float incomingDamageMultiplier);
        void OnAfterHit(ushort attackerId, DamageType damageType);
    }
}
