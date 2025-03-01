using DungeonMastersServer.Services;
using Riptide;

namespace DungeonMastersServer.MessageHandlers;

public class ChatMessageHandler
{

    [MessageHandler((ushort)ClientToServerId.sendChatMessage)]
    private static void HandleChatMessagePackage(ushort fromClient, Message message)
    {
        var chatMessage = message.GetString();
        
        ChatMessageService.Service.SendChatMessage(chatMessage, fromClient);
    }

}