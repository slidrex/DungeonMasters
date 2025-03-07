using DungeonMastersServer.Logger;
using DungeonMastersServer.MessageHandlers;
using DungeonMastersServer.Repositories;
using Riptide;

namespace DungeonMastersServer.Services;

public class ChatMessageService : SingletonService<ChatMessageService>
{
    public void SendChatMessage(string chatMessage, ushort fromClient)
    {
        var player = ClientRepository.Service.GetPlayer(fromClient);
        var playerNickname = player.Username;

        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerChatMessage);
        msg.AddString(playerNickname);
        msg.AddString(chatMessage);
        
        MessageLogger.Log("Sending player chat message");
        NetworkManager.Server.SendToAll(msg);
    }
    public void SendSystemChatMessage(string chatMessage)
    {
        var playerNickname = "system";

        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerChatMessage);
        msg.AddString(playerNickname);
        msg.AddString(chatMessage);

        MessageLogger.Log("Sending system chat message");
        NetworkManager.Server.SendToAll(msg);
    }
}