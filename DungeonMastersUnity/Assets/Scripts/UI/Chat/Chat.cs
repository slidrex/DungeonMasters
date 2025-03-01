using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Chat
{
    public class Chat : MonoBehaviour
    {
        [SerializeField] private ChatMessage messagePrefab;
        [SerializeField] private Transform content;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private GameObject chatPanel;
        [SerializeField] private ScrollRect scrollRect;

        public void AddMessage(string username, string message)
        {
            ChatMessage chatMessage = Instantiate(messagePrefab, content);
            chatMessage.SetMessage(message, null, username);
            scrollRect.verticalNormalizedPosition = 0;
            Debug.Log($"{username}: {message}");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Slash))
            {
                chatPanel.SetActive(!chatPanel.activeSelf);
            }
        }

        public void ShowChatPanel()
        {
            chatPanel.SetActive(true);
        }

        public void HideChatPanel()
        {
            chatPanel.SetActive(false);
        }
    }
}
