using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;
using TMPro;

namespace MoonKart.UI
{
    public class UIMainMenuView : UIView 
    {
        [TabGroup("General")] [SerializeField] private GameObject bg;
        [TabGroup("General")] [SerializeField] private CinemachineVirtualCamera camera;
        [TabGroup("General")] [SerializeField] private TextMeshProUGUI versionText;
        
        // PRIVATE MEMBERS
        [TabGroup("Button")][SerializeField] private UIButton dummyButton;
        [TabGroup("Button")][SerializeField] private UIButton settingButton;
        
        // UIView INTEFACE

        protected override void OnInitialize()
        {
            base.OnInitialize();

            //(Context.Player as PlayerData).Nickname = "Player" + UnityEngine.Random.Range(100, 96334); 
            versionText.text =  "Version " + PlayerPrefs.GetString("version", "");
            dummyButton.onClick.AddListener(OnDummyButton);
            settingButton.onClick.AddListener(OnSettingButton);
           
            Global.PlayerService.PlayerDataChanged += OnPlayerDataChanged;
            
        }

        protected override void OnDeinitialize()
        {
            dummyButton.onClick.RemoveListener(OnDummyButton);

            base.OnDeinitialize();
        }


        protected override void OnOpen()
        {
            base.OnOpen();
          
            if (bg)
                bg.SetActive(true);

            UpdatePlayer();
            Context.Previews.ShowPlayer(Context.Player.SelectedIndex, false,false);
        }

        protected override void OnClose()
        {
            base.OnClose();
                      
            if (bg)
                bg.SetActive(false);
        }

        protected override bool OnBackAction()
        {
            if (IsInteractable == false)
                return false;

            OnQuitButton();
            return true;
        }

        // PRIVATE METHODS

        private void OnSettingButton()
        {
            Open<UISettingsView>();
        }

        private void OnDummyButton()
        {
            Context.UI.OpenWithBackView<UIDummyView>(this);
            Close();
        }
        
        private void OnQuitButton()
        {
            var dialog = Open<UIYesNoDialogView>();

            dialog.Title.text = "EXIT GAME";
            dialog.Description.text = "Are you sure you want to exit the game?";

            dialog.YesButtonText.text = "EXIT";
            dialog.NoButtonText.text = "CANCEL";

            dialog.HasClosed += (result) =>
            {
                if (result == true)
                {
                    GameUI.Game.Quit();
                }
            };
        }

        private void OnPlayerDataChanged(PlayerData playerData)
        {
            UpdatePlayer();
        }

        private void UpdatePlayer()
        {
         //   player.SetData(Context.Player);
        }

  
    }
}