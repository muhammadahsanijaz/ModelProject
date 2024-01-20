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
        [TabGroup("Button")][SerializeField] private UIButton carButton;
        [TabGroup("Button")][SerializeField] private UIButton settingButton;
        [TabGroup("Button")][SerializeField] private UIButton allCardsButton;
        [TabGroup("Button")][SerializeField] private UIButton multiplayerButton;
        [TabGroup("Button")][SerializeField] private UIButton singlePlayerButton;
        [TabGroup("Button")][SerializeField] private UIButton changeNicknameButton;
        [TabGroup("Button")][SerializeField] private UIButton presetMakingButton;
        [TabGroup("Button")][SerializeField] private UIButton fuelButton;
        [TabGroup("Button")][SerializeField] private UIButton mergeButton;
        //[TabGroup("Button")][SerializeField] private UIPlayer player;
        
        // UIView INTEFACE

        protected override void OnInitialize()
        {
            base.OnInitialize();

            //(Context.Player as PlayerData).Nickname = "Player" + Random.Range(100, 96334); 
            versionText.text =  "Version " + PlayerPrefs.GetString("version", "");
            carButton.onClick.AddListener(OnGarageButton);
            allCardsButton.onClick.AddListener(OnAllCardsButton);
            multiplayerButton.onClick.AddListener(OnMultiplayerButton);
            singlePlayerButton.onClick.AddListener(OnSinglePlayerButton);
            changeNicknameButton.onClick.AddListener(OnChangeNicknameButton);
            presetMakingButton.onClick.AddListener(OnPresetsMakingButton);
            settingButton.onClick.AddListener(OnSettingButton);
            fuelButton.onClick.AddListener(OnFuelButton);
           mergeButton.onClick.AddListener(OnMergeButton);
           
            Global.PlayerService.PlayerDataChanged += OnPlayerDataChanged;
            
        }
        private void OnPresetsMakingButton()
        {
            OpenWithBackView<UIGarageView>(GameMode.PresetMaking);
            Close();
        }

        private void OnSinglePlayerButton()
        {
            OpenWithBackView<UIGarageView>(GameMode.Single);
            Close();
        }

        protected override void OnDeinitialize()
        {
            carButton.onClick.RemoveListener(OnGarageButton);
            carButton.onClick.RemoveListener(OnAllCardsButton);
            multiplayerButton.onClick.RemoveListener(OnMultiplayerButton);
            changeNicknameButton.onClick.RemoveListener(OnChangeNicknameButton);
            singlePlayerButton.onClick.RemoveListener(OnSinglePlayerButton);
            presetMakingButton.onClick.RemoveListener(OnPresetsMakingButton);
            settingButton.onClick.RemoveListener(OnSettingButton);
            fuelButton.onClick.RemoveListener(OnFuelButton);
            mergeButton.onClick.RemoveListener(OnMergeButton);
            Global.PlayerService.PlayerDataChanged -= OnPlayerDataChanged;

            base.OnDeinitialize();
        }

        private void OnMergeButton()
        {
            //var filterData = new SlotsActionData { cardType = typeof(Card), category = -1 };
           // OpenWithBackView<UICardMergingView>(filterData);
            Close();
        }

        private void OnFuelButton()
        {
            //var filterData = new SlotsActionData { cardType = typeof(Card), category = -1 };
            //OpenWithBackView<UIAutoVaultMenuView>(filterData);
            Close();
        }

        private void OnSettingButton()
        {
            Open<UISettingsView>();
        }

        protected override void OnOpen()
        {
            base.OnOpen();
          
            if (bg)
                bg.SetActive(true);

            UpdatePlayer();
            Context.Garage.ShowDriver(Context.Player.CarPresetIndex, false,false);
            //Global.PlayerService.CarSetupUpdated += Global.Settings.CarSetting.GetCarSetup(Context.Player.CarPresetIndex).UpdateCarSetup;
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

        private void OnGarageButton()
        {
            OpenWithBackView<UIGarageView>();
            Close();
        }
        
        private void OnAllCardsButton()
        {
            //var filterData = new SlotsActionData { cardType = typeof(Card), category = -1 };
            //OpenWithBackView<UICardsMenuView>(filterData,CardCallFrom.MyCard);
            Close();
        }


        private void OnMultiplayerButton()
        {
            OpenWithBackView<UIGarageView>( GameMode.Multiplayer);
            Close();
        }

        private void OnChangeNicknameButton()
        {
            var changeNicknameView = Open<UIChangeNicknameView>();
            changeNicknameView.SetData("CHANGE NICKNAME", false);
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