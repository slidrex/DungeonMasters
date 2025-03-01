using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Models.Player;
using DungeonMastersServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
