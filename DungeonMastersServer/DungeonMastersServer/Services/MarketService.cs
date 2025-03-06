﻿using System.Text.Json;
using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Repositories;
using Riptide;

namespace DungeonMastersServer.Services;

public class MarketService : SingletonService<MarketService>
{
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
    };
    
    public void SendMarketItems()
    {
        Console.WriteLine("Sending Market Items");
        var marketItems = MarketRepository.Service.GetMarketSlots();

        ushort slotsLength = (ushort)marketItems.Length;


        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.SEND_MARKETABLE_ITEMS);

        msg.AddUShort(slotsLength);

        foreach(var item in marketItems)
        {
            msg.AddString(item.Item.Title);
            msg.AddString(item.Item.GetDescription());
            msg.AddByte((byte)item.Item.SlotType);
            
            
            var itemStats = item.Item.GetStats();
            msg.AddInt(itemStats.Count);
            foreach (var itemStat in itemStats)
            {
                msg.AddString(itemStat.Key);
                msg.AddFloat(itemStat.Value);
            }
        }

        NetworkManager.Server.SendToAll(msg);
    }

    public void SendPlayerItems(ushort playerId)
    {
        var playerItems = ClientRepository.Service.GetPlayerItems(playerId);
        
        ushort slotsLength = (ushort)playerItems.Count;

        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.SEND_PLAYER_ITEMS);
        
        msg.AddUShort(slotsLength);

        foreach (var item in playerItems)
        {
            msg.AddString(item.Title);
            msg.AddString(item.GetDescription());
            msg.AddByte((byte)item.SlotType);
        }
        
        NetworkManager.Server.Send(msg, playerId);
    }
}