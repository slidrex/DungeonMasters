using TMPro;
using UI.Message;
using UnityEngine;
using Extensions;

namespace UI.Chat
{
    public class ChatMessage : MonoBehaviour
    {
        private TextMeshProUGUI _textMesh;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        public void SetMessage(string message, MessageStyle style = null, string userName = "")
        {
            style = style ?? MessageStyles.DefaultStyle;
            
            _textMesh.text = $"{userName} :{message}";
            _textMesh.ApplyStyle(style);
        }
    }
}