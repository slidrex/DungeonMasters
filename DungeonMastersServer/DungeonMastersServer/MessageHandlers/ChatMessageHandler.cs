using Riptide;

namespace DungeonMastersServer.MessageHandlers;

public class ChatMessageHandler : MessageHandler<ChatMessageHandler>
{
    [MessageHandler((ushort)ClientToServerId.sendChatMessage)]
    private static void HandleChatMessagePackage(ushort fromClient, Message message)
    {
        Singleton.HandleChatMessage(fromClient, message);
    }

    public void HandleChatMessage(ushort fromClient, Message message)
    {
        var username = GetUsername(fromClient);
        var chatMessage = message.GetString();
        
        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.playerChatMessage);
        msg.AddString(username);
        msg.AddString(chatMessage);
        
        NetworkManager.Server.SendToAll(msg);
    }

    private static string GetUsername(ushort playerId)
    {
        return PlayerMessageHandler.Players[playerId].Username;
    }
}