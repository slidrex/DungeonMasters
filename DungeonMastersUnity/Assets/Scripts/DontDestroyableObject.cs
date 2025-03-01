using UnityEngine;

public class DontDestroyableObject : MonoBehaviour
{
    private static DontDestroyableObject _instance;
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
