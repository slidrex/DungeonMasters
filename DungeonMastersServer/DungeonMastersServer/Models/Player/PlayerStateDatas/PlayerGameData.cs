using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using DungeonMastersServer.Models.InGameModels.Items.Armor;
using DungeonMastersServer.Services;
using Riptide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.Player.PlayerDatas
{

    enum PlayerLifeState
    {
        Alive = 0,
        Dead = 1,
    }
    enum PlayerDebuffState
    {
        
    }

    enum PlayerBuffState
    {
        StrengthLevel1 = 0,
        MoreHealth = 1,
        MoreGold = 2,
        MoreGoldInRoundStartLevel1 = 3
    }
    internal sealed class PlayerGameData : PlayerStateData
    {
        public float DefaultSpeed = 0.04f;
        public int Health { get; set; }
        
        public int MaxHealth { get; set; } 
        
        public PlayerLifeState LifeState { get; private set; }
        public bool EndTurn { get; set; }
        public int Gold { get; private set; }
        private List<Item> _items = new List<Item>(); 
        public Item HandItem { get; private set; }
        public Item CarriedArmorItem { get; private set; }
        private float _armorMultiplier { get; set; }
        private int _armorExtractor { get; set; }
        private float _damageMultiplier { get; set; }
        private int _damageAddUnits { get; set; }
        public void Damage(ushort attackerId, int damage, DamageType damageType)
        {

            var chest = new CactusChestplate();
            chest.GetStats();

            foreach (var item in _items)
            {

                if (item is IIncomingDamageMultiplier multiplier)
                {
                    damage = (int)(damage * multiplier.GetIncomingDamageMultiplier(attackerId, damageType));
                }
            }
            if (CarriedArmorItem != null)
            {
                ((IArmor)CarriedArmorItem).OnBeforeHit(attackerId, damageType, out float multiplier);
                damage = (int)(damage * multiplier);
            }
            if (damageType == DamageType.Physical)
            {
                damage -= GetMasterArmor();
            }
            Health -= damage;
            
            var targetHealth = Health;

            var targetMaxHealth = MaxHealth;

            if (Health <= 0)
                KillPlayer();

            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.GAME_HURT_PLAYER);
            msg.AddInt(targetHealth);
            msg.AddInt(targetMaxHealth);

            NetworkManager.Server.Send(msg, PlayerClient.ClientId);
        }
        public void AddArmorMultiplier(float multiplier)
        {
            _armorMultiplier += multiplier;
        }
        public void AddArmorUnit(int units)
        {
            _armorExtractor += units;
        }
        public void AddDamageMultiplier(float multiplier)
        {
            _damageMultiplier += multiplier;
        }
        public void AddDamageUnit(int units)
        {
            _damageAddUnits += units;
        }
        public bool AttachItem(Item item)
        {
            if(item is IMountable mountable){
                mountable.OnMount();
            }

            if (_items.Count == 9)
            {
                return false;
            }
            _items.Add(item);
            return true;
        }
        public void DettachItem(Item item)
        {
            if (item is IMountable mountable)
            {
                mountable.OnUnmount();
            }
            _items.Remove(item);
        }
        public int GetMasterArmor()
        {
            int masterArmor = 0;
            
            if(CarriedArmorItem != null)
            {
                masterArmor += ((IArmor)CarriedArmorItem).Armor;
            }
            masterArmor = (int)((masterArmor + _armorExtractor) * _armorMultiplier);

            return masterArmor;
        }
        public int GetMasterDamage()
        {
            int masterDamage = 10;
            if (HandItem != null && HandItem is IHitItem hit)
            {
                masterDamage = hit.Damage;
            }

            return (int)((masterDamage + _damageAddUnits) * _damageMultiplier);
        }
        public void OnNewRoundStarted()
        {
            foreach (var item in _items)
            {
                var roundDependant = item as IRoundDependant;
                roundDependant?.OnRoundStarted();
            }
        }

        public override void EnterState()
        {
            MaxHealth = 100;
            Health = MaxHealth;
            LifeState = PlayerLifeState.Alive;
            Gold = 5;
        }
        public void AddGold(int amount)
        {
            Gold += amount;
        }
        
        private void KillPlayer()
        {
            LifeState = PlayerLifeState.Dead;
        }

        public List<Item> GetPlayerItems()
        {
            return _items;
        }
    }
}
