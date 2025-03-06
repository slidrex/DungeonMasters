using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items.Abstract
{
    public static class ItemStatsExtension
    {
        internal static Dictionary<string, float> GetStats(this Item item)
        {
            Dictionary<string, float> stats = new();

            if (item is IArmor armor) stats.Add("Armor", armor.Armor);
            if (item is IHitItem damage) stats.Add("Damage", damage.Damage);

            var addStats = item.GetAdditionalStats();
            if (addStats != null)
            {
                foreach(var stat in addStats)
                {
                    stats.Add(stat.Name, stat.Value);
                }
            }

            return stats;
        }
    }
}
