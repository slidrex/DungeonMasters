using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Repositories;
using Riptide;

namespace DungeonMastersServer.Services;

public class ChatMessageService : SingletonService<ChatMessageService>
{
    public void SendChatMessage(ushort fromClient, string chatMessage)
    {
        var player = ClientRepository.Service.GetPlayer(fromClient);
        var playerNickname = player.Username;

        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerChatMessage);
        msg.AddString(playerNickname);
        msg.AddString(chatMessage);
        
        NetworkManager.Server.SendToAll(msg);
    }
}