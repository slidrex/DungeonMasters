using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;

namespace DungeonMastersServer.Models.InGameModels.Items.Weapons
{

    internal sealed class OsirisSceptre : Item, IHitItem, IHealItem
    {
        protected override SlotType SlotType => SlotType.Weapon;
        public int Damage => 30;

        public int Heal => 10;
        
        internal override string Title => "Osiris Sceptre";
        
        public void OnHit(ushort targetId)
        {
            
        }

        public void OnHeal(ushort targetId)
        {
            
        }

        internal override string GetDescription()
        {
            return "Can damage enemy or heal yourself";
        }
    }
}