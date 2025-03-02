using DungeonMastersServer.MessageHandlers;
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

        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.GAME_HURT_PLAYER);
        msg.AddString(attackerPlayer.Username);
        msg.AddString(targetPlayer.Username);
        
        NetworkManager.Server.SendToAll(msg);
    }
}