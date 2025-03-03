using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Models.InGameModels.Market;
using DungeonMastersServer.Models.Player;
using DungeonMastersServer.Models.Player.PlayerDatas;
using DungeonMastersServer.Repositories;
using Riptide;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonMastersServer.Services;

internal enum RoundState
{
    BuyStage = 0,
    GameStage = 1
}
internal enum DamageType
{
    Physical,
    Magical,
    ExtractHealth
}
/*
internal enum ItemId
{
    MagicApple_LEVEL1 = 1,
    GoldenScarab = 2,
    PlusDamageMinusArmor = 3,
    TotAmulet = 4,
    OsirisSceptre = 5,
    SetSceptre = 6,
    WoodenSword = 7,
    CactusChestplate = 8,
    HekClothes = 9,
    RaShield = 10,
    WipeDamageIdMagical = 11
}
*/

public class GameService : SingletonService<GameService>
{
    private RoundState _roundState = RoundState.BuyStage;
    
    public int RoundCounter { get; private set; } = 1;
    public void HitRequest(ushort target, ushort attacker, int damage)
    {
        var attackerPlayer = ClientRepository.Service.GetPlayer(attacker);
        var targetPlayer = ClientRepository.Service.GetPlayer(target);

        ChatMessageService.Service.SendSystemChatMessage($"{attackerPlayer.Username} hit {targetPlayer.Username}!");

        targetPlayer.GetGameData().Damage(attacker, damage, DamageType.Physical);

    }

    public async Task StartNewRound()
    {
        ClientService.Service.TransportAllPlayers(10000);
        var players = ClientRepository.Service.GetPlayers();

        foreach (var player in players)
        {
            var playerGameData = player.Value.GetGameData();
            AddGoldInRoundStart(playerGameData);
            
            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.GAME_NEWROUND);
            msg.AddInt(player.Key);
            msg.AddInt(playerGameData.Gold);
            
            NetworkManager.Server.Send(msg, player.Key);
        }
        
        RoundCounter++;
        Console.WriteLine($"New round: {RoundCounter}");
        ChatMessageService.Service.SendSystemChatMessage($"Round {RoundCounter} started");
        
        await Task.Delay(10000);
        
        _ = OnBuyStageEnd();
    }
    private async Task OnBuyStageEnd()
    {
        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.GAME_BUY_STAGE_END);
        NetworkManager.Server.SendToAll(msg);

        await Task.Delay(30000);
        
        if (!ClientRepository.Service.AreAllPlayersEndTurn())
            _ = OnAllPlayersPressedReady();
    }
    public void PlayerPressedReady(ushort playerId)
    {
        ClientRepository.Service.SetEndTurn(playerId, true);
        if (ClientRepository.Service.AreAllPlayersEndTurn())
            _ = OnAllPlayersPressedReady();
    }
    private async Task OnAllPlayersPressedReady()
    {
        await Task.Delay(5000);

        _ = StartNewRound();
    }

    private void AddGoldInRoundStart(PlayerGameData playerGameData)
    {

       playerGameData.AddGold(10);
    }
    public async Task SetTimer(byte seconds, Action onTimerEnd)
    {
        
        await Task.Delay(seconds * 1000);
        var msg2 = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.REMOVE_UI_TIMER);
        NetworkManager.Server.SendToAll(msg2);
        onTimerEnd.Invoke();
    }
}