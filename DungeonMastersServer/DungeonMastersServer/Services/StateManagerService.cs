using DungeonMastersServer.MessageHandlers;
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
                GameStarted?.Invoke();
                var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.LOBBY_GameStarted);
                NetworkManager.Server.SendToAll(msg);
            }
            CurrentState = state;
        }
        
    }
}
