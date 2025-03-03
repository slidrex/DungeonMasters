using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Chat
{
    public class Chat : MonoBehaviour
    {
        private const float ChatVisibleDuration = 6f;
        private Coroutine _hideChatCoroutine;
        
        [SerializeField] private CanvasGroup chatCanvasGroup;
        [SerializeField] private ChatMessage messagePrefab;
        [SerializeField] private Transform content;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private GameObject chatPanel;
        [SerializeField] private ScrollRect scrollRect;

        public void AddMessage(string username, string message)
        {
            if (_hideChatCoroutine != null)
                StopCoroutine(_hideChatCoroutine);
            
            ChatMessage chatMessage = Instantiate(messagePrefab, content);
            chatMessage.SetMessage(message, null, username);
            scrollRect.verticalNormalizedPosition = 0;
            Debug.Log($"{username}: {message}");
            
            _hideChatCoroutine = StartCoroutine(PresentChat());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Slash))
            {
                if (chatPanel.activeSelf)
                {
                    HideChatPanel();
                }
                else
                {
                    ShowChatPanel();
                    inputField.ActivateInputField();
                }
            }
        }
        
        private IEnumerator PresentChat()
        {
            ShowChatPanel();

            float timer = 0;
            float fadeTimer = 0;
            while (timer < ChatVisibleDuration)
            {
                if (inputField.isFocused)
                {
                    chatCanvasGroup.alpha = 1;
                    yield break;
                }

                if (timer >= 3)
                {
                    fadeTimer += Time.deltaTime;
                    chatCanvasGroup.alpha = Mathf.Lerp(1, 0.1f, fadeTimer / ChatVisibleDuration);
                }
                
                timer += Time.deltaTime;
                yield return null;
            }
            HideChatPanel();
            _hideChatCoroutine = null;
        }

        public void ShowChatPanel()
        {
            SetActivePanel(true);
        }

        public void HideChatPanel()
        {
            SetActivePanel(false);
        }

        private void SetActivePanel(bool active)
        {
            chatPanel.SetActive(active);
        }
    }
}
