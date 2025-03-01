using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class AuthScreen : MonoBehaviour
    {
        [SerializeField] private Button connectButton;
        [SerializeField] private TMP_InputField ipInput;
        [SerializeField] private TMP_InputField usernameInput;

        private void OnEnable()
        {
            connectButton.onClick.AddListener(ConnectToServer);
        }

        private void OnDisable()
        {
            connectButton.onClick.RemoveListener(ConnectToServer);
        }

        private void ConnectToServer()
        {
            NetworkManager.Singleton.ConnectToServer(usernameInput.text, ipInput.text);
        }
    }
}