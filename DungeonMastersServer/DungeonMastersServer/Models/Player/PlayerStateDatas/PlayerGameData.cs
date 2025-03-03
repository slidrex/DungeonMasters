using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private List<PlayerBuffState> _buffs = new List<PlayerBuffState>();
        public Item HandItem { get; private set; }
        public void OnHitTaken(ushort attackerId)
        {
            foreach(var item in _items)
            {
                var armor = item as IArmor;
                armor?.OnHit(attackerId);
            }
        }
        public void OnAttack()
        {

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

        public List<PlayerBuffState> GetBuffs()
        {
            return _buffs;
        }

        public void AddBuff(PlayerBuffState buff)
        {
            _buffs.Add(buff);
        }
        public void AddGold(int amount)
        {

            if (_buffs.Contains(PlayerBuffState.MoreGold))
                Gold += amount + 5;
            else
                Gold += amount;
        }

        public void RemoveGold(int amount)
        {
            Gold -= amount;
        }
        
        public void KillPlayer()
        {
            LifeState = PlayerLifeState.Dead;
        }

        public void HealPlayer(int amount)
        {
            Health += amount;
        }
    }
}
