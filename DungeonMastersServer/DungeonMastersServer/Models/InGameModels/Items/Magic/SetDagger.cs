using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using DungeonMastersServer.Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items.Magic
{
    class SetDagger : Item, IMountable
    {
        internal override SlotType SlotType => SlotType.Magic;

        internal override string Title => "Set's Dagger";

        private int _armor = 10;
        private int _damage = 15;

        public void OnMount()
        {
            Data.AddArmorUnit(-_armor);
            Data.AddDamageUnit(_damage);
        }

        public void OnUnmount()
        {
            Data.AddArmorUnit(_armor);
            Data.AddDamageUnit(-_damage);
        }

        internal override string GetDescription()
        {
            return "Gain damage and lose armor";
        }
    }
}
