using System;
using Riptide;
using UI.Chat;
using UnityEngine;
using UnityEngine.Serialization;

namespace Multiplayer.MessageHandlers
{
    public class PlayerMessageHandler : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private Chat chat;
        private static PlayerMessageHandler _singleton;

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
        public void HandleReady(bool isReady){
            FindFirstObjectByType<Lobby>().SetButtonReadyStatus(isReady);
        }

        private void Awake()
        {
            _singleton = this;
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
            chat.AddMessage(username, message);
        }
        private void GameStarted(){
            FindFirstObjectByType<StateManager>().SwitchToGame();
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
            if (PlayerManager.list.TryGetValue(message.GetUShort(), out PlayerController player)){
                player.Move(message.GetVector2());
            }
        }
        [MessageHandler((ushort)ServerToClientId.LOBBY_responseSetReady)]
        private static void HandleReadyResponse(Message message){
            Singleton.HandleReady(message.GetBool());
        }
        [MessageHandler((ushort)ServerToClientId.LOBBY_GameStarted)]
        private static void GameStarted(Message message){
            Singleton.GameStarted();
        }
    }
}