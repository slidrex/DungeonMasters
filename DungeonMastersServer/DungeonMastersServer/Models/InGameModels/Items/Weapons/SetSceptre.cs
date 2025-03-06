using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;

namespace DungeonMastersServer.Models.InGameModels.Items.Weapons
{

    internal sealed class SetSceptre : Item, IRoundDependant, IUsable, IMountable
    {
        internal override SlotType SlotType => SlotType.Weapon;

        internal override string Title => "Sceptre of Set's";

        public bool IsActive { get; private set; } = false;

        private int _damage = 15;

        public void OnMount()
        {
            Data.AddDamageUnit(_damage);
        }

        public void OnUnmount()
        {
            Data.AddDamageUnit(-_damage);
        }
        
        public void OnUse()
        {
            IsActive = !IsActive;
        }
        public void OnRoundStarted()
        {
            if (IsActive)
            {
                Data.Health -= 15;
                Data.AddGold(5);
            }
        }

        internal override string GetDescription()
        {
            return $"Can be activated, if active, than every round - 15hp and +5 gold";
        }
    }
}