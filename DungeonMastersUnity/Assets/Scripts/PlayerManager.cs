using UnityEngine;
using System.Collections.Generic;
using Riptide;
using UnityEngine.Scripting.APIUpdating;
using System;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static Dictionary<ushort, PlayerController> list = new();
    public PlayerController LocalPlayer { get; private set; }
    
    public void Despawn(ushort id)
    {
        var player = list[id];
        list.Remove(id);
        Destroy(player.gameObject);
    }

    public void DespawnAllPlayers()
    {
        foreach (var (key, value) in list)
        {
            Destroy(value.gameObject);
        }
        list.Clear();
    }
    public void Spawn(ushort id, string username, Vector3 position)
    {
        PlayerController player = Instantiate(GameLogic.Singleton.PlayerPrefab, position, Quaternion.identity).GetComponent<PlayerController>();
        if(id == NetworkManager.Singleton.Client.Id){
            player.SetAsLocal();
            LocalPlayer = player;
        }
        player.SetUsername(username);
        //FindFirstObjectByType<FollowingCamera>().SetTarget(player.transform);

        player.name = $"Player {id} (username)";
        player.Id = id;

        list.TryAdd(id, player);
        foreach (var pl in list)
        {
            Debug.Log($"SPAWN {pl} ");
        }
    }
}
