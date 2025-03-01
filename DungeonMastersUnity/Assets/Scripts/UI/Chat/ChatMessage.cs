using TMPro;
using UI.Message;
using UnityEngine;
using Extensions;
using UnityEngine.Serialization;

namespace UI.Chat
{
    public class ChatMessage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        
        public void SetMessage(string message, MessageStyle style = null, string userName = "")
        {
            style = style ?? MessageStyles.DefaultStyle;
            
            textMesh.text = $"{userName} :{message}";
            textMesh.ApplyStyle(style);
        }
    }
}