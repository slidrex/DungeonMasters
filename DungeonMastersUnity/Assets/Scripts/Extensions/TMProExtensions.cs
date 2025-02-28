using TMPro;
using UI.Message;

namespace Extensions
{
    public static class TMProExtensions
    {
        public static void ApplyStyle(this TextMeshProUGUI textMesh, MessageStyle style)
        {
            textMesh.fontSize = style.FontSize;
            textMesh.color = style.Color;
        }
    }
}