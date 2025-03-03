﻿using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;

namespace DungeonMastersServer.Models.InGameModels.Items.Weapons
{

    internal sealed class OsirisSceptre : Item, IUsable, IMountable
    {
        protected override SlotType SlotType => SlotType.Weapon;
        private int _damage = 20;

        private int _heal => 10;
        
        internal override string Title => "Osiris Sceptre";
        
        public void OnUse()
        {
            Data.Health += _heal;
        }

        public void OnMount()
        {
            Data.AddDamageUnit(_damage);
        }

        public void OnUnmount()
        {
            Data.AddDamageUnit(-_damage);
        }

        internal override string GetDescription()
        {
            return "Can damage enemy or heal yourself";
        }
    }
}