﻿using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Models.Player;
using DungeonMastersServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonMastersServer.Models.InGameModels.Items.Abstract;
using DungeonMastersServer.Models.Player.PlayerDatas;

namespace DungeonMastersServer.Repositories
{
    class ClientRepository : SingletonService<ClientRepository>
    {
        private readonly Dictionary<ushort, PlayerClient> _players = new();
        public void AddPlayer(ushort id, PlayerClient client)
        {
            _players.Add(id, client);
        }
        public KeyValuePair<ushort, PlayerClient>[] GetPlayers()
        {
            return _players.ToArray();
        }

        public List<Item> GetPlayerItems(ushort id)
        {
            var player = GetPlayer(id);
            var playerGameData = player.GetGameData();
            return playerGameData.GetPlayerItems();
        }
        
        public bool AreAllPlayersEndTurn()
        {
            return _players.Values.All(player => player.GetGameData().EndTurn);
        }

        public void SetEndTurn(ushort id, bool endTurn)
        {
            var player = GetPlayer(id);
            var playerData = player.GetGameData();
            playerData.EndTurn = endTurn;
        }

        public int GetPlayerMaxHealth(ushort id)
        {
            var player = GetPlayer(id);
            var playerData = player.GetGameData();
            return playerData.MaxHealth;
        }
        public PlayerClient GetPlayer(ushort id)
        {
            return _players[id];
        }
        public void RemovePlayer(ushort id)
        {
            _players.Remove(id);
        }
        public void SetPlayer(ushort id, PlayerClient player)
        {
            _players[id] = player;
        }

        public void ClearPlayers()
        {
            _players.Clear();
        }
    }
}
