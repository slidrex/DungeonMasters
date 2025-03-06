using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using DungeonMastersServer.Services;

namespace DungeonMastersServer.Models.InGameModels.Items.Armor
{

    internal sealed class RaShield : ArmorItem
    {
        public override int Armor => 30;
        private int _goldGrants = 5;
        internal override string Title => "Ra's Shield";        
        internal override string GetDescription() => $"Grants {_goldGrants} gold every incoming hit";

        public override void OnAfterHit(ushort attackerId, DamageType damageType)
        {
            Data.AddGold(_goldGrants);
        }
    }
}