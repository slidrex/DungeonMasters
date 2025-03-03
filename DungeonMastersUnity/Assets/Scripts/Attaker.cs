using System;
using Riptide;
using UnityEngine;

public class Attaker : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Hit();
    }

    private void Hit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity);

            Debug.Log(hit.transform);
            
            if (hit.collider != null && hit.collider.TryGetComponent(out PlayerController player))
            {
                if (!player.IsLocal)
                {
                    Message msg = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.GAME_REQUESTHIT);
                    msg.AddUShort(player.Id);
                    NetworkManager.Singleton.Client.Send(msg);
                }
            }
        }
    }
}
