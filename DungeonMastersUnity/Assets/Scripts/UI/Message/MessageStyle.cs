using UnityEngine;

namespace UI.Message
{
    public class MessageStyle
    {
        public int FontSize {get; private set;}
        public Color Color {get; private set;}
        
        public MessageStyle(int fontSize, Color color)
        {
            FontSize = fontSize;
            Color = color;
        }
    }

    public static class MessageStyles
    {
        public static readonly MessageStyle DefaultStyle = new MessageStyle(12, Color.white);
        
    }
}
