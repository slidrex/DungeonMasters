using UnityEngine;
using System.Collections.Generic;
using Riptide;
using UnityEngine.Scripting.APIUpdating;
using System;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static Dictionary<ushort, PlayerController> PlayerDictionary {get; private set;}
    public PlayerController LocalPlayer { get; private set; }


    private void Start()
    {
        PlayerDictionary ??= new Dictionary<ushort, PlayerController>();
    }

    public void Despawn(ushort id)
    {
        var player = PlayerDictionary[id];
        PlayerDictionary.Remove(id);
        Destroy(player.gameObject);
    }
    
    public void Spawn(ushort id, string username, Vector3 position)
    {
        PlayerController player = Instantiate(GameLogic.Singleton.PlayerPrefab, position, Quaternion.identity).GetComponent<PlayerController>();
        if(id == NetworkManager.Singleton.Client.Id){
            player.SetAsLocal();
            LocalPlayer = player;
            FindFirstObjectByType<FollowingCamera>().SetTarget(player.transform);
        }
        player.SetUsername(username);
        

        player.name =username;
        player.Id = id;

        PlayerDictionary.TryAdd(id, player);
        foreach (var keyValuePair in PlayerDictionary)
        {
            //Debug.Log(keyValuePair);
        }
        //Debug.Log("_____________________________________");
    }
}
