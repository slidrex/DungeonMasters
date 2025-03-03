﻿using DungeonMastersServer.Models.InGameModels.Items.Interfaces;

namespace DungeonMastersServer.Models.InGameModels.Items
{

    internal sealed class GoldenScarab : Item, IRoundDependant
    {
        internal override ushort Id => 8;

        internal override string Title => "Golden Scarab";
        
        public void OnRoundStarted()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 100);
            
            if (randomNumber == 7)
                Data.AddGold(Data.Gold);
            else if (randomNumber == 9)
                Data.Health -= Data.MaxHealth / 2;
            else if (randomNumber > 60)
                Data.AddGold(15);
        }

        internal override string GetDescription()
        {
            return "This golden artefact has 100 edges, every round you rolling it. If you get 7 - you double your current gold, if number more than 60, you get plus 15 gold, but if you get 9, you lose half of your max health";
        }
    }
}