using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Services;
namespace DungeonMastersServer.Models.InGameModels.Items.Armor
{
    class CactusChestplate : ArmorItem
    {
        internal override string Title => "Cactus armor";
        public override int Armor => 25;

        private int _reflectDamage = 5; // Дамаг который возвращается аттакеру
        internal override string GetDescription() => $"Reflects {_reflectDamage} damage to attacker";
        public override void OnAfterHit(ushort attackerId, DamageType damageType)
        {
            //бьем аттакера маг уронов на урон равный _reflectDamage
            BreakItem();
        }
    }
}
