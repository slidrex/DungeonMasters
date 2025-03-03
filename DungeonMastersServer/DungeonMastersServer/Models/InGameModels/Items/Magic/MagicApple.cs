using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items.Magic
{
    internal sealed class MagicApple : Item, IRoundDependant
    {
        protected override SlotType SlotType => SlotType.Magic;
        internal override ushort Id => 0;
        internal override string Title => "Magic Apple";
        public void OnRoundStarted()
        {
            Data.AddGold(5);
        }

        internal override string GetDescription()
        {
            return "Grants 5 gold every round";
        }
    }
}
