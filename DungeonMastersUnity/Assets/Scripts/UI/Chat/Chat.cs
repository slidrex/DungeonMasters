using System;
using System.Collections;
using States;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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

        private InputSystem_Actions _inputSystemActions;
        private InputAction _closeAction;
        private void Awake()
        {
            ContextStateManager.RegisterState(State.Chat, chatPanel.gameObject);
            _inputSystemActions = new InputSystem_Actions();
            _closeAction = _inputSystemActions.UI.Cancel;
        }

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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ContextStateManager.ClearState();
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
            ContextStateManager.SetState(State.Chat);
        }

        public void HideChatPanel()
        {
            ContextStateManager.ClearState();
        }
    }
}
