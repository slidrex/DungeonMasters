namespace DungeonMastersServer.Models.Player.PlayerDatas
{
    class PlayerLobbyData : PlayerStateData
    {
        public float LobbySpeed = 0.04f;
        public bool IsReady { get; private set; }
        public void SetReadyStatus(bool isReady)
        {
            IsReady = isReady;
        }
    }
}
