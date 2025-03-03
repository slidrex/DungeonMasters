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
    class PlusDamageMinusArmor : Item, IMountable
    {
        protected override SlotType SlotType => SlotType.Magic;

        internal override string Title => "Some item";

        private int RemoveArmorValue = 10;
        private int AddDamageValue = 15;

        public void OnMount()
        {
            Data.AddArmorUnit(-RemoveArmorValue);
            Data.AddDamageUnit(AddDamageValue);
            
        }

        public void OnUnmount()
        {
            Data.AddArmorUnit(RemoveArmorValue);
            Data.AddDamageUnit(-RemoveArmorValue);
        }

        internal override string GetDescription()
        {
            return "";
        }
    }
}
