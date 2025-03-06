using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items.Abstract
{
    class ItemStat
    {
        public string Name { get; private set; }
        public float Value { get; private set; }
        public ItemStat(string name, float value)
        {
            Name = name;
            Value = value;
        }
        public static ItemStat Create(string name, float value)
        {
            return new ItemStat(name, value);
        }
    }
}
