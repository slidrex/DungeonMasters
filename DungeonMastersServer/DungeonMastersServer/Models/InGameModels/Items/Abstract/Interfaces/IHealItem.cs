namespace DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;

public interface IHealItem
{
    int Heal { get; }

    void OnHeal(ushort targetId)
    {
        
    }
}