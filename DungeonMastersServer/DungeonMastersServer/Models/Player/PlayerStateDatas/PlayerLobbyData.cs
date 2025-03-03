namespace DungeonMastersServer.Models.Player.PlayerDatas
{
    internal sealed class PlayerLobbyData : PlayerStateData
    {
        public float LobbySpeed = 0.04f;
        public bool IsReady { get; private set; }
        public bool IsLoadedToGameScene { get; private set; }
        public void SetReadyStatus(bool isReady)
        {
            IsReady = isReady;
        }
        public void SetAsLoad()
        {
            IsLoadedToGameScene = true;
        }
    }
}
