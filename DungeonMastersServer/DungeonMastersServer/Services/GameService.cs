using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Models.Player;
using DungeonMastersServer.Models.Player.PlayerDatas;
using DungeonMastersServer.Repositories;
using Riptide;

namespace DungeonMastersServer.Services;

public enum RoundState
{
    RoundActive = 0,
    RoundEnded = 1
}

public class GameService : SingletonService<GameService>
{
    private RoundState _roundState = RoundState.RoundEnded;

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

    public void StartNewRound()
    {
        ClientService.Service.TransportAllPlayers();
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
        
        _roundState = RoundState.RoundActive;
        RoundCounter++;
        Console.WriteLine($"New round: {RoundCounter}");
        ChatMessageService.Service.SendSystemChatMessage($"Round {RoundCounter} started");

        //Сначала фризим игроков на 10 секунд (идет закупка). После этого времени отправляем пакет {закупка окончена}.

        //Фриз кончился .отсчитываем 20 секунд раунда. После закупки и начала раунда, игроки могут отправить пакет I'm ready.
        //Если за 20 секунд раунда не все игроки отправили I'm ready отсчитывается 10 секунд после чего новый раунд начинается принудительно.
        //Если все игроки нажали Ready, все таймеры сбрасываются, включается новый 5 секундный таймер после чего новый раунд
    }
    private void OnBuyStageEnd()
    {

    }
    private void PlayerPressedReady()
    {
        //if some player press ready
    }
    private void OnAllPlayersPressedReady()
    {
        //If all players press ready.
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