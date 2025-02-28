using UnityEngine;
using Riptide;

public class PlayerController : MonoBehaviour
{

    private bool[] inputs = new bool[4];


    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            inputs[0] = true;

        if (Input.GetKey(KeyCode.A))
            inputs[1] = true;

        if (Input.GetKey(KeyCode.S))
            inputs[2] = true;

        if (Input.GetKey(KeyCode.D))
            inputs[3] = true;
    }

    private void FixedUpdate()
    {
        if(inputs[0] || inputs[1] || inputs[2] || inputs[3] == true){
        SendInput();

        }

        for (int i = 0; i < inputs.Length; i++)
            inputs[i] = false;
    }

    #region Messages
    private void SendInput()
    {
        Message message = Message.Create(MessageSendMode.Unreliable, (ushort)ClientToServerId.sendMoveInputs);
        message.AddBools(inputs, false);
        NetworkManager.Singleton.Client.Send(message);
    }
    #endregion
}
