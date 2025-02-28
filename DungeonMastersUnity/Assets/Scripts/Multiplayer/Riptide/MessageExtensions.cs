using Riptide;
using UnityEngine;

public static class MessageExtension{

    public static Message AddVector2(this Message message, Vector2 value){
        message.AddFloat(value.x);
        message.AddFloat(value.y);
        return message;
    }
    public static Vector2 GetVector2(this Message message){
        
        Vector2 vector = new Vector2(message.GetFloat(), message.GetFloat());

        return vector;
    }

}