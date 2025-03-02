using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Models.Player.PlayerDatas;
using DungeonMastersServer.Repositories;
using Riptide;

namespace DungeonMastersServer.Services;

public class GameService : SingletonService<GameService>
{
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

    private void KillPlayer(PlayerGameData playerGameData)
    {
        playerGameData.KillPlayer();

        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.GAME_PLAYER_DEAD);

        NetworkManager.Server.SendToAll(msg);
    }
}