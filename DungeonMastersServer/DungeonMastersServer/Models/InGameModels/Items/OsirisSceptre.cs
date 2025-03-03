using DungeonMastersServer.Models.InGameModels.Items.Interfaces;

namespace DungeonMastersServer.Models.InGameModels.Items
{

    internal sealed class OsirisSceptre : Item, IHitItem, IHealItem
    {
        public int Damage => 30;

        public int Heal => 10;

        internal override ushort Id => 4;
        
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