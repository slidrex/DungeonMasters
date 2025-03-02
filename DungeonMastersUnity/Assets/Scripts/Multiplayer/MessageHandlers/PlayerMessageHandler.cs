using Riptide;
using UI;
using UI.Chat;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Multiplayer.MessageHandlers
{
    public class PlayerMessageHandler : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        private Chat chat;
        private static PlayerMessageHandler _singleton;
        private HealthBar _healthBar;

        public static PlayerMessageHandler Singleton
        {
            get => _singleton;
            set
            {
                if (_singleton == null)
                    _singleton = value;
                else if (_singleton != value)
                {
                    Debug.Log($"{nameof(PlayerMessageHandler)} instance already exists, destroying duplicate!");
                    Destroy(value);
                }
            }
        }

        public void HandleReady(bool isReady)
        {
            FindFirstObjectByType<Lobby>().SetButtonReadyStatus(isReady);
        }

        private void Awake()
        {
            _singleton = this;
        }

        private void Start()
        {
            chat = FindFirstObjectByType<Chat>();
            _healthBar = FindFirstObjectByType<HealthBar>();
        }

        private void Spawn(ushort id, string username, Vector3 position)
        {
            playerManager.Spawn(id, username, position);
        }

        private void Despawn(ushort id)
        {
            playerManager.Despawn(id);
        }

        private void SendMsg(string username, string message)
        {
            if (chat == null) chat = FindFirstObjectByType<Chat>();

            chat.AddMessage(username, message);
        }

        private void GameStarted()
        {
            playerManager.DespawnAllPlayers();
            SceneManager.LoadScene(2);
            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.LOBBY_LOAD_PLAYERS);
            var msg2 = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.LOBBY_SWITCH_TO_GAME);
            msg.AddString(NetworkManager.Singleton.UserName);
            NetworkManager.Singleton.Client.Send(msg);
            NetworkManager.Singleton.Client.Send(msg2);
        }

        [MessageHandler((ushort)ServerToClientId.playerChatMessage)]
        public static void SendChatMessage(Message message)
        {
            _singleton.SendMsg(message.GetString(), message.GetString());
        }

        [MessageHandler((ushort)ServerToClientId.playerConnected)]
        private static void SpawnPlayer(Message message)
        {
            _singleton.Spawn(message.GetUShort(), message.GetString(), Vector3.zero);
        }

        [MessageHandler((ushort)ServerToClientId.playerDisconnected)]
        private static void DespawnPlayer(Message message)
        {
            _singleton.Despawn(message.GetUShort());
        }

        [MessageHandler((ushort)ServerToClientId.playerMovement)]
        private static void PlayerMovement(Message message)
        {
            if (PlayerManager.list.TryGetValue(message.GetUShort(), out PlayerController player))
            {
                player.Move(message.GetVector2());
            }
        }

        [MessageHandler((ushort)ServerToClientId.LOBBY_responseSetReady)]
        private static void HandleReadyResponse(Message message)
        {
            Singleton.HandleReady(message.GetBool());
        }

        [MessageHandler((ushort)ServerToClientId.LOBBY_GameStarted)]
        private static void GameStarted(Message message)
        {
            Singleton.GameStarted();
        }

        [MessageHandler((ushort)ServerToClientId.playerTeleport)]
        private static void PlayerTeleport(Message message)
        {
            var fromClient = message.GetUShort();
            var pos = message.GetVector2();

            var player = PlayerManager.list[fromClient];
            player.Move(pos);
        }

        [MessageHandler((ushort)ServerToClientId.GAME_HURT_PLAYER)]
        private static void GameHurtPlayer(Message message)
        {
            var currentHealth = message.GetInt();
            var maxHealth = message.GetInt();

            Singleton._healthBar.SetHealth(currentHealth, maxHealth);
        }
    }
}