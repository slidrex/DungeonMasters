using DungeonMastersServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces
{
    interface IIncomingDamageMultiplier
    {
        /// <summary>
        /// Before damage hits player it multiplies by the value of this method
        /// </summary>
        /// <returns></returns>
        float GetIncomingDamageMultiplier(ushort attackerId, DamageType damageType);
    }
}
