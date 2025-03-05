using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Models.Player.PlayerDatas;
using DungeonMastersServer.Repositories;
using Riptide;

enum GameState
{
    InLobby = 0,
    InGame = 1
}

namespace DungeonMastersServer.Services
{
    public delegate void OnGameStateChanged<GameState>(GameState oldState, GameState newState);
    public delegate void OnGameStarted<GameState>();
    internal class StateManagerService : SingletonService<StateManagerService>
    {
        public GameState CurrentState { get; private set; }

        public event OnGameStateChanged<GameState> GameStateChanged;
        public event OnGameStarted<GameState> GameStarted; 

        public void SetState(GameState state)
        {
            if (state == CurrentState) throw new Exception($"Set the same state ({CurrentState} to {state}) which is not allowed.");
            GameStateChanged?.Invoke(CurrentState, state);
            if (CurrentState == GameState.InLobby && state == GameState.InGame)
            {
                OnGameStarted();
            }
            CurrentState = state;
        }
        private void OnGameStarted()
        {
            GameStarted?.Invoke();
            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.GAME_STARTED);
            NetworkManager.Server.SendToAll(msg);

            var players = ClientRepository.Service.GetPlayers();

            foreach (var player in players)
            {
                player.Value.SetPlayerStateData(new PlayerGameData());
            }
            Console.WriteLine("Started game rounds loop");
            MarketService.Service.SendMarketItems();
            _ = GameService.Service.StartNewRound();
        }
        
    }
}
