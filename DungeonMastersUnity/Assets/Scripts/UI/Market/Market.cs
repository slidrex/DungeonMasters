using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public enum SlotType
{
    Magic = 0,
    Armor = 1,
    Weapon = 2,
    Artifact = 3
}
namespace UI.Market
{
    internal class Market : MonoBehaviour
    {
        public List<MarketSlot> MarketSlots; // слотыыыыыыыы мазагина!
        
        [SerializeField] private Button marketButton;
        [SerializeField] private GameObject marketPanel;
        
        [Header("Section Buttons")]
        [SerializeField] private Button magicButton;
        [SerializeField] private Button armorButton;
        [SerializeField] private Button weaponButton;
        [SerializeField] private Button artifactButton;
        [SerializeField] private ItemStore _store;
        private void Awake()
        {
            MarketSlots = GetComponentsInChildren<MarketSlot>(true).ToList();
        }
        private void Start()
        {
            magicButton.onClick.AddListener(OnMagicBtnClick);   
            armorButton.onClick.AddListener(OnArmorBtnClick);
            weaponButton.onClick.AddListener(OnWeaponBtnClick);
            artifactButton.onClick.AddListener(OnArtifactBtnClick);
        }
        private void OnMagicBtnClick()
        {
            RenderItems(SlotType.Magic);
        }
        private void OnArmorBtnClick()
        {
            RenderItems(SlotType.Armor);
        }
        private void OnWeaponBtnClick()
        {
            RenderItems(SlotType.Weapon);
        }
        private void OnArtifactBtnClick()
        {
            RenderItems(SlotType.Artifact);
        }
        private void RenderItems(SlotType type){
            var magic = _store.GetItemsOfType(type);
            foreach(var slot in MarketSlots){slot.SetItem(null);}
            int currentSlot = 0;
            foreach(var item in magic){
                var sprite = item.Sprite;
                if(sprite == null) sprite = Resources.Load<Sprite>($"Items/None");
                MarketSlots[currentSlot].SetItem(sprite);
                currentSlot++;
            }
        }
        
        
        
        
        private void OnEnable()
        {
            marketButton.onClick.AddListener(OnMarketButtonClick);
        }

        private void OnDisable()
        {
            marketButton.onClick.RemoveListener(OnMarketButtonClick);
        }

        

        #region MarketPanel
            private void OnMarketButtonClick() =>
                SetActivePanel(!marketPanel.activeSelf);
            public void ShowPanel() =>
                SetActivePanel(true);
            public void HidePanel() =>
                SetActivePanel(false);
            private void SetActivePanel(bool active) =>
                marketPanel.SetActive(active);
        #endregion
    }
}