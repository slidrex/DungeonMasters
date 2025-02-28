using UnityEngine;
using System.Collections.Generic;
using Riptide;
using UnityEngine.Scripting.APIUpdating;
using System;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public static Dictionary<ushort, Player> list = new Dictionary<ushort, Player>();
    [SerializeField] private TMP_Text _username;
    public ushort Id { get; private set; }
    public bool IsLocal { get; private set; }

    private string username;

    private void OnDestroy()
    {
        list.Remove(Id);
    }
    public void SetUsername(string username)
    {
        _username.text = username;
    }
    public static void Despawn(ushort id)
    {
        var player = list[id];
        list.Remove(id);
        Destroy(player.gameObject);
    }
    public static void Spawn(ushort id, string username, Vector3 position)
    {

        Player player = Instantiate(GameLogic.Singleton.PlayerPrefab, position, Quaternion.identity).GetComponent<Player>();


        player.IsLocal = id == NetworkManager.Singleton.Client.Id;
        
        player.SetUsername(username);


        player.name = $"Player {id} (username)";
        player.Id = id;
        player.username = username;

        list.Add(id, player);

        
    }
    public void Move(Vector2 position){
        transform.position = position;
    }

    [MessageHandler((ushort)ServerToClientId.playerConnected)]
    private static void SpawnPlayer(Message message)
    {
        Spawn(message.GetUShort(), message.GetString(), Vector3.zero);
    }
    [MessageHandler((ushort)ServerToClientId.playerDisconnected)]
    private static void DespawnPlayer(Message message)
    {
        Despawn(message.GetUShort());
    }
    [MessageHandler((ushort)ServerToClientId.playerMovement)]
    private static void PlayerMovement(Message message)
    {
        if (list.TryGetValue(message.GetUShort(), out Player player)){
            player.Move(message.GetVector2());
            Console.WriteLine("move");
        }
    }
    
}
