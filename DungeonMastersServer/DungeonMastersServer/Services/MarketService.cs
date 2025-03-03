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
        var marketItems = MarketRepository.Service.GetMarketSlots();

        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.SEND_MARKETABLE_ITEMS);

        var stringMarketItems = marketItems
            .Select(item => JsonSerializer.Serialize(item))
            .ToArray();
        
        msg.AddStrings(stringMarketItems);
        NetworkManager.Server.SendToAll(msg);
    }
}