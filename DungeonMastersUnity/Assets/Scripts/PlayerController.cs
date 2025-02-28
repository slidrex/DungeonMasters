using UnityEngine;
using Riptide;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private bool[] inputs = new bool[4];
    [SerializeField] private TextMeshProUGUI userName;
    public ushort Id { get; set; }
    public bool IsLocal { get; set; }

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

    public void SetUsername(string username)
    {
        userName.text = username;
    }
    private void OnDestroy()
    {
        PlayerManager.list.Remove(Id);
    }

    private void FixedUpdate()
    {
        if(inputs[0] || inputs[1] || inputs[2] || inputs[3] == true){
            SendInput();
        }

        for (int i = 0; i < inputs.Length; i++)
            inputs[i] = false;
    }
    public void Move(Vector2 position){
        transform.position = position;
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
