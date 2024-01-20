using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MoonKart.UI
{
    public class UITopBarMenu :  UIWidget
    {

        [SerializeField] private UIView _uiView;
        [SerializeField] private UIButton addDiamondButton;
        [SerializeField] private UIButton addCoinButton;
        [SerializeField] private UIButton addCryptoCoinButton;
        [SerializeField] private UIButton settingButton;

        private void Start()
        {
            addDiamondButton.onClick.AddListener(OnAddDiamondButtonPressed);
            addCoinButton.onClick.AddListener(OnAddCoinButtonPressed);
            addCryptoCoinButton.onClick.AddListener(OnAddCryptoButtonPressed);
            settingButton.onClick.AddListener(OnSettingButtonPressed);
        }

      

        private void OnSettingButtonPressed()
        {
            //(Context.UI as MenuUI)?.Open<UISettingsView>();
        }

        private void OnAddCryptoButtonPressed()
        {
            //(Context.UI as MenuUI)?.OpenWithBackView<UIStoreView>(_uiView);
        }

        private void OnAddCoinButtonPressed()
        {
            //(Context.UI as MenuUI)?.OpenWithBackView<UIStoreView>(_uiView);
        }

        private void OnAddDiamondButtonPressed()
        {
            //(Context.UI as MenuUI)?.OpenWithBackView<UIStoreView>(_uiView);
        }
    }
}