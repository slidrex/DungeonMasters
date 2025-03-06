using System;
using System.Collections.Generic;
using Riptide;
using UI;
using UI.Chat;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Compositors;
using UI.Market;

namespace Multiplayer.MessageHandlers
{
    public class PlayerMessageHandler : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        private Chat _chat;
        private static PlayerMessageHandler _singleton;
        [SerializeField] private HealthBar _healthBar;
        private ItemStore _itemStorage;

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
            _chat = FindFirstObjectByType<Chat>();
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
            if (_chat == null) _chat = FindFirstObjectByType<Chat>();

            _chat.AddMessage(username, message);
        }

        private void LoadGameScene()
        {
            StartCoroutine(SceneLoader.LoadSceneAsync(2, () =>
            {
                var msg = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.LOBBY_GAME_SCENE_LOADED);
                NetworkManager.Singleton.Client.Send(msg);
            }));
            
        }
        public void HandleMarketable(string title, string description, byte type)
        {
            if(_itemStorage == null){
                _itemStorage = FindFirstObjectByType<ItemStore>();
            }
            _itemStorage.AddItem(title, description, (SlotType)type);
        }
        
        [MessageHandler((ushort)ServerToClientId.SEND_MARKETABLE_ITEMS)]
        public static void HandleMarketableItems(Message message)
        {
            ushort writeCount = message.GetUShort();
            
            for(int i = 0; i < writeCount; i++){
                Singleton.HandleMarketable(message.GetString(), message.GetString(), message.GetByte());
            }

        }

        public void HandlePlayerItems(string title, string description, byte type)
        {
            
        }
        
        [MessageHandler((ushort)ServerToClientId.SEND_PLAYER_ITEMS)]
        private static void HandlePlayerItemsPackage(Message message)
        {
            ushort writeCount = message.GetUShort();

            for (int i = 0; i < writeCount; i++)
            {
                Singleton.HandlePlayerItems(message.GetString(), message.GetString(), message.GetByte());
            }
        }
        
        [MessageHandler((ushort)ServerToClientId.SEND_ITEM_STATS)]
        private static void HandleItemStats(Message message)
        {
            string itemString = message.GetString();

            var itemStats = JsonUtility.FromJson<Item>(itemString);
        }
        
        [MessageHandler((ushort)ServerToClientId.playerChatMessage)]
        public static void SendChatMessage(Message message)
        {
            _singleton.SendMsg(message.GetString(), message.GetString());
        }

        [MessageHandler((ushort)ServerToClientId.spawnPlayer)]
        private static void SpawnPlayer(Message message)
        {
            var spawnId = message.GetUShort();
            string username = message.GetString();
            Debug.Log("spawn " + username);
            _singleton.Spawn(spawnId, username, Vector3.zero);
        }

        [MessageHandler((ushort)ServerToClientId.playerDisconnected)]
        private static void DespawnPlayer(Message message)
        {
            _singleton.Despawn(message.GetUShort());
        }

        [MessageHandler((ushort)ServerToClientId.playerMovement)]
        private static void PlayerMovement(Message message)
        {
            if (PlayerManager.PlayerDictionary.TryGetValue(message.GetUShort(), out PlayerController player))
            {
                player.Move(message.GetVector2());
            }
        }

        [MessageHandler((ushort)ServerToClientId.LOBBY_responseSetReady)]
        private static void HandleReadyResponse(Message message)
        {
            Singleton.HandleReady(message.GetBool());
        }

        [MessageHandler((ushort)ServerToClientId.LOBBY_SWITCH_TO_GAME_SCENE)]
        private static void SwitchToGame(Message message)
        {
            Singleton.LoadGameScene();
        }
        [MessageHandler((ushort)ServerToClientId.GAME_STARTED)]
        private static void GameStarted(Message message)
        {
            
        }
        [MessageHandler((ushort)ServerToClientId.playerTeleport)]
        private static void PlayerTeleport(Message message)
        {
            var fromClient = message.GetUShort();
            var pos = message.GetVector2();

            var player = PlayerManager.PlayerDictionary[fromClient];
            player.Move(pos);
        }

        [MessageHandler((ushort)ServerToClientId.GAME_HURT_PLAYER)]
        private static void GameHurtPlayer(Message message)
        {
            var currentHealth = message.GetInt();
            var maxHealth = message.GetInt();
            if(Singleton._healthBar == null){
                Singleton._healthBar = FindFirstObjectByType<HealthBar>();
            }
            Singleton._healthBar.SetHealth(80, 100);
        }
        
        [MessageHandler((ushort)ServerToClientId.GAME_NEWROUND)]
        private static void GameNewRound(Message message)
        {
            byte round = message.GetByte();
            int goldValue = message.GetInt();

            GameCompositor.Singleton.SetUIRoundIndex(round);
            GameCompositor.Singleton.SetGold(goldValue);

            var market = GameCompositor.Singleton.Market;
            market.GetMarketButton().gameObject.SetActive(true);
            market.DefaultRenderItems();
        }

        [MessageHandler((ushort)ServerToClientId.GAME_BUY_STAGE_END)]
        private static void GameBuyStageEnd(Message message)
        {
            GameCompositor.Singleton.Market.HidePanel();
        }
        
        
        //TODO_____TODO_____TODO_____TODO_____TODO_____TODO_____TODO_____TODO_____TODO_____TODO_____TODO_____TODO_____TODO
        // СДЕЛАТЬ НА КЛИЕНТЕ ТЕ ЖЕ КЛАССЫ MarketSlot и Item и прочую хуйню чтобы нормально сериализовать строку в объект
        /*[MessageHandler((ushort)ServerToClientId.SEND_MARKETABLE_ITEMS)]
        private static void GetMarketableItems(Message message)
        {
            var marketableItemsString = message.GetStrings();

            var marketableItems = new List<MarketSlot>();
            foreach (var itemJson in marketableItemsString)
            {
                Debug.Log(itemJson);
                var marketSlot = JsonUtility.FromJson<MarketSlot>(itemJson);
                marketableItems.Add(marketSlot);
            }
            
        }*/
    }
}