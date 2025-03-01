using UnityEngine;
using Riptide;
using Riptide.Utils;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

enum ClientToServerId
{
    sendName = 0,
    sendMoveInputs = 1,
    sendChatMessage = 2,

    LOBBY_requestSetReady = 50,
    LOBBY_loadLobbyScene = 51,
    
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
    playerTeleport = 11,

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
    public string UserName { get; private set; }
    public Client Client;


    private void Awake()
    {
        Singleton = this;
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
    public void ConnectToServer(string username, string ip)
    {
        if(username == "" || username.Length < 3){
            username = "Player" + UnityEngine.Random.Range(0, 10000);
        }
        Client.Connect($"{ip}:{_port}"); 
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.sendName);
        message.AddString(username);
        NetworkManager.Singleton.Client.Send(message);
        UserName = username;
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
        SceneManager.LoadScene(1);
        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.LOBBY_loadLobbyScene);
        msg.AddString(UserName);
        NetworkManager.Singleton.Client.Send(msg);
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
