using UnityEngine;
using Riptide;
using Riptide.Utils;
using System;
using UnityEngine.UI;
using TMPro;
enum ClientToServerId
{
    sendName = 0,
    sendMoveInputs = 1,
    sendChatMessage = 2,

    LOBBY_requestSetReady = 50,

    GAME_REQUESTHIT = 100,
    GAME_REQUEST_USE_ABILITY = 101,
    GAME_REQUEST_BUY_ABILITY = 102
}


enum ServerToClientId
{
    playerConnected = 0,
    playerDisconnected = 1,
    playerMovement = 2,
    playerChatMessage = 3,

    setAllPlayerPositions = 10,

    LOBBY_responseSetReady = 50,
    LOBBY_GameStarted = 51,

    GAME_NEWROUND = 100,
    GAME_RESPONSE_HIT = 101,
    GAME_RESPONSE_USE_ABILITY = 102,
    GAME_RESPONSE_BUY_ABILITY = 103,
    GAME_HURT_PLAYER = 104,
}

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _singleton;
    public static NetworkManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(NetworkManager)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }
    private string _ip;
    [SerializeField] private ushort _port;
    [SerializeField] private Button _connectButton;
    [SerializeField] private TMP_InputField _ipInput;
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private GameObject _authScreen;
    [SerializeField] private GameObject _gameScreen;

    public Client Client;


    private void Awake()
    {
        Singleton = this;
    }
    private void OnEnable()
    {
        _connectButton.onClick.AddListener(ConnectToServer);
    }
    private void OnDisable()
    {
        _connectButton.onClick.RemoveListener(ConnectToServer);
    }
    void Start()
    {
        RiptideLogger.Initialize(Debug.Log, false);

        Client = new Client();
        
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FailedToConnect;
        Client.ClientDisconnected += PlayerLeft;
        Client.Disconnected += DidDisconnect;  
    }
    private void ConnectToServer()
    {
        string username = _usernameInput.text;
        if(username == "" || username.Length < 3){
            username = "Player" + UnityEngine.Random.Range(0, 10000);
        }
        Client.Connect($"{_ipInput.text}:{_port}"); 
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.sendName);
        message.AddString(username);
        NetworkManager.Singleton.Client.Send(message);
        _authScreen.SetActive(false);
        _gameScreen.SetActive(true);
    }
    
    public static void SendMsg(string chatMessage) {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.sendChatMessage);
        message.AddString(chatMessage);
        NetworkManager.Singleton.Client.Send(message);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Client.Update();
    }
    private void OnApplicationQuit()
    {
        Client.Disconnect();   
    }
    private void DidConnect(object sender, EventArgs e)
    {

    }
    private void DidDisconnect(object sender, EventArgs e)
    {

    }

    private void FailedToConnect(object sender, EventArgs e)
    {

    }

    private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
    {

    }

    
}
