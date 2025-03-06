using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces;

namespace DungeonMastersServer.Models.InGameModels.Items.Magic
{

    internal sealed class PharaohHeart : Item, IUsable
    {
        internal override SlotType SlotType => SlotType.Magic;
        
        internal override string Title => "Pharaoh's Heart";

        public void OnUse()
        {
            Data.MaxHealth += 20;
            Data.Health += 20;
        }

        internal override string GetDescription()
        {
            return "Grants you +20 max health then you consume this";
        }
    }
}