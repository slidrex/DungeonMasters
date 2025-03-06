using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using DungeonMastersServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items.Abstract
{
    internal abstract class ArmorItem : Item, IArmor
    {
        internal override SlotType SlotType => SlotType.Armor;
        public abstract int Armor { get; }

        public virtual void OnAfterHit(ushort attackerId, DamageType damageType)
        {

        }

        public virtual void OnBeforeHit(ushort attackerId, DamageType damageType, out float incomingDamageMultiplier)
        {
            incomingDamageMultiplier = 1;
        }
    }
}
