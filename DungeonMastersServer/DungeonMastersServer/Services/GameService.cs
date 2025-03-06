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
    None = 0,
    BuyStage = 1,
    GameStage = 2
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
    private Task _currentRoundTask;
    private CancellationTokenSource _cts = new();
    internal RoundState RoundState { get; private set; } = RoundState.None;

    public byte RoundCounter { get; private set; } = 0;
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
        RoundCounter++;
        Console.WriteLine($"New round: {RoundCounter}");
        ClientRepository.Service.SetAllEndTurnFalse();
        foreach (var player in players)
        {
            var playerGameData = player.Value.GetGameData();
            AddGoldInRoundStart(playerGameData);
            
            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.GAME_NEWROUND);
            msg.AddByte(RoundCounter);
            msg.AddInt(playerGameData.Gold);
            msg.AddUShort((ushort)players.Length);

            NetworkManager.Server.Send(msg, player.Key);
        }
        
        
        ChatMessageService.Service.SendSystemChatMessage($"Round {RoundCounter} started");
        
        await Task.Delay(10000);
        RoundState = RoundState.BuyStage;
        _ = OnBuyStageEnd();
    }
    private async Task OnBuyStageEnd()
    {
        _cts?.Cancel(); 
        _cts = new CancellationTokenSource();

        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.GAME_BUY_STAGE_END);
        NetworkManager.Server.SendToAll(msg);

        RoundState = RoundState.GameStage;

        try
        {
            await Task.Delay(30000, _cts.Token);

            if (!ClientRepository.Service.AreAllPlayersEndTurn(out ushort readyPlayers, out ushort readyPlayersCount))
                _ = OnAllPlayersPressedReady();
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("Задержка отменена.");
            return;
        }
    }

    public void StopBuyStageTimer()
    {
        _cts.Cancel();
    }

    public void PlayerPressedReady(ushort playerId)
    {
        ClientRepository.Service.SetEndTurn(playerId, true);
        bool areAllReady = ClientRepository.Service.AreAllPlayersEndTurn(out ushort readyPlayers, out ushort allPlayers);

        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.GAME_TURN_END_RESPONSE);
        foreach(var player in ClientRepository.Service.GetPlayers())
        {
            msg.AddBool(ClientRepository.Service.IsPlayerEndTurn(player.Key));
            msg.AddUShort(readyPlayers);
            msg.AddUShort(allPlayers);
            NetworkManager.Server.Send(msg, player.Key);
        }
        

        if (areAllReady)
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
        RoundState = RoundState.None;
        await Task.Delay(seconds * 1000);
        var msg2 = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.REMOVE_UI_TIMER);
        NetworkManager.Server.SendToAll(msg2);
        onTimerEnd.Invoke();
    }
}