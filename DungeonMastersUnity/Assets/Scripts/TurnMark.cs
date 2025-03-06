using Riptide;
using TMPro;
using UnityEngine;

public class TurnMark : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _notReadyMark;
    [SerializeField] private TextMeshProUGUI _readyMark;
    public void SetMark(bool isReady, int readyCount, int playersCount)
    {
        _readyMark.gameObject.SetActive(isReady);
        _notReadyMark.gameObject.SetActive(!isReady);

        if(isReady){
            _readyMark.text = $"Turn end voted.\nWait others ({readyCount}/{playersCount})";
        }
        else{
            _readyMark.text = $"\"T\" to end turn\n ({readyCount}/{playersCount})";
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            var msg = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.GAME_REQUEST_END_TURN);
            NetworkManager.Singleton.Client.Send(msg);
        }
    }
}
