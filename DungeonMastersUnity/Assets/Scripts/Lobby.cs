using Riptide;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    [SerializeField] private Button _readyButton;
    [SerializeField] private TextMeshProUGUI _readyText;
    [SerializeField] private Color _readyColor;
    [SerializeField] private Color _notReadyColor;
    private bool _isReady;
    private void Start(){
        SetButtonReadyStatus(false);
    }
    public void SetButtonReadyStatus(bool isReady){
        _readyText.text = isReady ? "Ready" : "Not ready";
        var colors = _readyButton.colors;
        colors.normalColor = isReady ? _readyColor : _notReadyColor;
        _readyButton.colors = colors;
        _isReady = isReady;
    }
    
    private void OnEnable()
    {
        _readyButton.onClick.AddListener(Server_SendClick);
    }
    
    
        
    private void OnDisable()
    {
        _readyButton.onClick.RemoveListener(Server_SendClick);
    }
    private void Server_SendClick(){
        var msg = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.LOBBY_requestSetReady);
        msg.AddBool(!_isReady);
        NetworkManager.Singleton.Client.Send(msg);
    }
}
