using System.Text.Json;
using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Models.InGameModels.Market;
using DungeonMastersServer.Repositories;
using Riptide;

namespace DungeonMastersServer.Services;

public class MarketService : SingletonService<MarketService>
{
    public void SendMarketItems()
    {
        Console.WriteLine("Sending Market Items");
        var marketItems = MarketRepository.Service.GetMarketSlots();

        Console.WriteLine(marketItems.Length);
        
        foreach (var marketItem in marketItems)
        {
            Console.WriteLine($"{marketItem.Item.Title} {marketItem.Item.GetDescription()} {marketItem.BuyPrice}");
        }
        
        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.SEND_MARKETABLE_ITEMS);

        var stringMarketItems = marketItems
            .Select(item => JsonSerializer.Serialize(item))
            .ToArray();
        
        foreach (var marketItem in stringMarketItems)
        {
            Console.WriteLine(marketItem);
        }
        
        msg.AddStrings(stringMarketItems);
        NetworkManager.Server.SendToAll(msg);
    }
}