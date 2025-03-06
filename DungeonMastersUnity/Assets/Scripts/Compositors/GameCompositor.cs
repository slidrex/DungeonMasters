using System;
using TMPro;
using UI.Chat;
using UI.Market;
using UnityEngine;

namespace Compositors
{
    public class GameCompositor : MonoBehaviour
    {
        [field: SerializeField] public Market Market { get; private set; }
        [field: SerializeField] public Chat Chat { get; private set; }
        [SerializeField] private TextMeshProUGUI _roundIndex;
        [SerializeField] private TextMeshProUGUI _goldIndex;
        [field: SerializeField] public TurnMark TurnMark {get; private set;}

        public void SetUIRoundIndex(byte index){
            _roundIndex.text = index.ToString();
        }
        public void SetGold(int gold){
            _goldIndex.text = gold.ToString();
        }
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

