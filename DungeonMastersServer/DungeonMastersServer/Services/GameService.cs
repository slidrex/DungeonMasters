using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Models.Player;
using DungeonMastersServer.Models.Player.PlayerDatas;
using DungeonMastersServer.Repositories;
using Riptide;

namespace DungeonMastersServer.Services;

public enum RoundState
{
    BuyStage = 0,
    GameStage = 1
}

public class GameService : SingletonService<GameService>
{
    private RoundState _roundState = RoundState.BuyStage;

    public int RoundCounter { get; private set; } = 1;
    public void HitRequest(ushort target, ushort attacker, int damage)
    {
        var attackerPlayer = ClientRepository.Service.GetPlayer(attacker);
        var targetPlayer = ClientRepository.Service.GetPlayer(target);
        
        ClientRepository.Service.DamagePlayer(target, damage);
        ChatMessageService.Service.SendSystemChatMessage($"{attackerPlayer.Username} hit {targetPlayer.Username}!");

        var targetPlayerGameData = targetPlayer.GetGameData();
        var targetHealth = targetPlayerGameData.Health;
        
        var targetMaxHealth = targetPlayerGameData.MaxHealth;

        if (targetPlayerGameData.Health <= 0)
            KillPlayer(targetPlayerGameData);
        
        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.GAME_HURT_PLAYER);
        msg.AddInt(targetHealth);
        msg.AddInt(targetMaxHealth);
        
        NetworkManager.Server.Send(msg, target);
    }
    public void OnPlayerHitDone()
    {

    }

    private void KillPlayer(PlayerGameData playerGameData)
    {
        playerGameData.KillPlayer();

        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.GAME_PLAYER_DEAD);

        NetworkManager.Server.SendToAll(msg);
    }

    private void HealPlayer(PlayerGameData playerGameData, int healAmount)
    {
        playerGameData.HealPlayer(healAmount);
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
        
        //Сначала фризим игроков на 10 секунд (идет закупка). После этого времени отправляем пакет {закупка окончена}.

        //Фриз кончился .отсчитываем 20 секунд раунда. После закупки и начала раунда, игроки могут отправить пакет I'm ready.
        //Если за 20 секунд раунда не все игроки отправили I'm ready отсчитывается 10 секунд после чего новый раунд начинается принудительно.
        //Если все игроки нажали Ready, все таймеры сбрасываются, включается новый 5 секундный таймер после чего новый раунд
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
        var playerBuffs = playerGameData.GetBuffs();
        if (playerBuffs.Contains(PlayerBuffState.MoreGoldInRoundStartLevel1))
            playerGameData.AddGold(10);
        else 
            playerGameData.AddGold(5);
    }
    public async Task SetTimer(byte seconds, Action onTimerEnd)
    {
        
        await Task.Delay(seconds * 1000);
        var msg2 = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.REMOVE_UI_TIMER);
        NetworkManager.Server.SendToAll(msg2);
        onTimerEnd.Invoke();
    }
    /*
    public async Task GameLoop()
    {
        if (_roundState == RoundState.RoundActive)
        {
            await Task.Delay(40000);
            _roundState = RoundState.RoundEnded;
            Console.WriteLine("Round end");
            _ = GameLoop();
        } else if (_roundState == RoundState.RoundEnded)
        {
            NewRound();
            _ = GameLoop();
        }
    }
    */
}