using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Market
{
    internal class Market : MonoBehaviour
    {
        public static List<MarketSlot> MarketSlots; // слотыыыыыыыы мазагина!
        
        [SerializeField] private Button marketButton;
        [SerializeField] private GameObject marketPanel;
        
        [Header("Section Buttons")]
        [SerializeField] private Button magicButton;
        [SerializeField] private Button armorButton;
        [SerializeField] private Button weaponButton;
        [SerializeField] private Button artifactButton;


        private void OnMagicBtnClick()
        {

        }
        private void OnArmorBtnClick()
        {
            
        }
        private void OnWeaponBtnClick()
        {
            
        }
        private void OnArtifactBtnClick()
        {
            
        }
        
        
        private void Awake()
        {
            MarketSlots = GetComponentsInChildren<MarketSlot>().ToList();
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