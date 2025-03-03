using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using DungeonMastersServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items.Armor
{
    //Passive item
    class WipeDamageIfMagical : Item, IIncomingDamageMultiplier
    {
        protected override SlotType SlotType => SlotType.Magic;


        internal override string Title => "Some item";

        public float GetIncomingDamageMultiplier(ushort attackerId, DamageType damageType)
        {
            if(damageType == DamageType.Magical)
            {
                return 0;
            }
            return 1;
        }

        internal override string GetDescription()
        {
            throw new NotImplementedException();
        }
    }
}
