namespace DungeonMastersServer.Models.InGameModels.Items.Interfaces;

public interface IHealItem
{
    int Heal { get; }

    void OnHeal(ushort targetId)
    {
        
    }
}