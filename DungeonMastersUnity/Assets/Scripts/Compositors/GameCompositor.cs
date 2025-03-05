using System;
using UI.Chat;
using UI.Market;
using UnityEngine;

namespace Compositors
{
    public class GameCompositor : MonoBehaviour
    {
        [field: SerializeField] public Market Market { get; private set; }
        [field: SerializeField] public Chat Chat { get; private set; }
        
        private static GameCompositor _singleton;
        public static GameCompositor Singleton
        {
            get => _singleton;
            private set
            {
                if (_singleton == null)
                    _singleton = value;
                else if (_singleton != value)
                {
                    Debug.Log($"{nameof(GameCompositor)} instance already exists, destroying duplicate!");
                    Destroy(value);
                }
            }
        }
    
        private void Awake()
        {
            Singleton = this;
        }
    }
}

