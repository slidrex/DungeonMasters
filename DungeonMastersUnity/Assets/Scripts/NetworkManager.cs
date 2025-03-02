using System;
using Riptide;
using Riptide.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
            username = "Player" + Random.Range(0, 10000);
        }
        Client.Connect($"{ip}:{_port}"); 
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.sendName);
        message.AddString(username);
        Singleton.Client.Send(message);
        UserName = username;
    }
    
    public static void SendMsg(string chatMessage) {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.sendChatMessage);
        message.AddString(chatMessage);
        Singleton.Client.Send(message);
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
        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.LOBBY_LOAD_PLAYERS);
        msg.AddString(UserName);
        Singleton.Client.Send(msg);
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
