using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Market
{
    internal class MarketSlot : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        private Button _button;
        
        // тута ваш айтем должын бить!!!
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        // тут нужен айтем естестенно
        public void SetItem(Sprite sprite)
        {
            itemImage.sprite = sprite;
            itemImage.enabled = true;
        }

        public void ClearItem()
        {
            // here you need to erase your item   
            itemImage.sprite = null;
            itemImage.enabled = false;
        }

        private void OnClick()
        {
            // туто оброботка нажатея на придмет
        }
        
        private void OnEnable()
        {
           _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
           _button.onClick.RemoveListener(OnClick);
        }
        
    }
}