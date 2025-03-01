using TMPro;
using UnityEngine;

namespace UI.Chat
{
    public class ChatInputHandler : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Chat chat;

        private void OnSubmit(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            
            NetworkManager.SendMsg(text);
            inputField.text = string.Empty;
        }

        private void OnEnable()
        {
            inputField.onSubmit.AddListener(OnSubmit);
        }

        private void OnDisable()
        {
            inputField.onSubmit.RemoveListener(OnSubmit);
        }
    }
}
