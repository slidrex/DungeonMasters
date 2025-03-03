using DungeonMastersServer.Models.InGameModels.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items
{
    internal sealed class WoodenSword : Item, IHitItem
    {
        internal override ushort Id => throw new NotImplementedException();

        internal override string Title => throw new NotImplementedException();

        public void OnHit(ushort targetId)
        {
            throw new NotImplementedException();
        }

        internal override string GetDescription()
        {
            throw new NotImplementedException();
        }
    }
}
