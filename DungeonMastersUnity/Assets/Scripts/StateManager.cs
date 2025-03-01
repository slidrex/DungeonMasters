using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _deactivateOnGameStarted;
    [SerializeField] private GameObject[] _activateOnGameStarted;
    public void SwitchToGame(){
        foreach(var obj in _deactivateOnGameStarted){
            obj.SetActive(false);
        }
        foreach(var obj in _activateOnGameStarted){
            obj.SetActive(true);
        }
    }
}
