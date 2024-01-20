using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace MoonKart.UI
{
    public class UIGarageView : UICloseView
    {
        [TabGroup("General")] [SerializeField] private GameObject bg;
        [TabGroup("General")] [SerializeField] private CinemachineVirtualCamera camera;
        [TabGroup("General")] [SerializeField] private UIButton _settingButton;

        [Header("Slots")] [TabGroup("General")] [SerializeField]
        private UIButton[] powerupSlots;

        [TabGroup("General")] [SerializeField] private UIButton[] techSlots;

        // PRIVATE MEMBERS

        [TabGroup("Debug")] [SerializeField] private TMP_InputField maxPlayer;
        [TabGroup("Debug")] [SerializeField] private TMP_InputField maxLap;

        [SerializeField] private UICar _carPanel;
        // [SerializeField] private UIButton _nextCarButton;
        // [SerializeField] private UIButton _previousCarButton;
        //[SerializeField] private UIButton _selectButton;


        [TabGroup("GameType")] [SerializeField]
        private GameObject muliplayerPanel;

        [TabGroup("GameType")] [SerializeField]
        private UIButton _startRoomButton;

        [TabGroup("GameType")] [SerializeField]
        private UIButton _nextButton;

        [TabGroup("GameType")] [SerializeField]
        private GameObject singleplayerPanel;

        [TabGroup("GameType")] [SerializeField]
        private UIButton _singleGameplayButton;

        [TabGroup("GameType")] [SerializeField]
        private UIBehaviour _presetsPanel;

        [TabGroup("GameType")] [SerializeField]
        private UIButton _savePresetsButton;

        [TabGroup("GameType")] [SerializeField]
        private UIButton _autovaultButton;

        //[SerializeField] private UIBehaviour _selectedGroup;
        [SerializeField] private UIList _selectionDots;

        [Space] [SerializeField] private float _closeDelayAfterSelection = 1;


        private string _previewCar;
        private bool _isRefreshing;
        private bool _uiPrepared;
        private bool _joinLobbyState;
        private int currentPreset;
        private GameMode _gameMode = GameMode.Single;


        protected override void OnInitialize()
        {
            base.OnInitialize();

            _selectionDots.SelectionChanged += OnSelectionChanged;


            // _nextCarButton.onClick.AddListener(OnNextCarButton);
            // _previousCarButton.onClick.AddListener(OnPreviousCarButton);
            //_selectButton.onClick.AddListener(OnSelectButton);
            _startRoomButton.onClick.AddListener(OnStartRoomButton);
            _nextButton.onClick.AddListener(OnNextButton);
            _settingButton.onClick.AddListener(OnSettingButton);
            _singleGameplayButton.onClick.AddListener(OnSinglePlayerButton);
            _autovaultButton.onClick.AddListener(OnAutoVaultButton);
            _savePresetsButton.onClick.AddListener(OnPresetsButton);
        }

        private void OnPresetsButton()
        {
            if (!CheckIfPresetChanged())
            {
                if (CheckIfPresetEmpty())
                {
                    return;
                }
            }


            var dialog = Open<UIYesNoDialogView>();

            dialog.Title.SetTextSafe("Presets " + (currentPreset + 1));
            dialog.Description.SetTextSafe("Are you sure you want to Save the Preset?");

            dialog.YesButtonText.SetTextSafe("Yes");
            dialog.NoButtonText.SetTextSafe("No");

            dialog.HasClosed += (result) =>
            {
                if (result == true)
                {
                    Context.Settings.CarSetting.GetCarSetup(_previewCar).SaveCarSetup();
                }
            };
        }

        private bool CheckIfPresetChanged()
        {
            bool isChanged = false;
            var carSetup = Global.Settings.CarSetting.GetCarSetup(Context.Player.CarPresetIndex);
            var savedCarSetup = PersistentStorage.GetObjectWithJsonUtility<CarSetup>(carSetup.ID);
            return isChanged;
        }

        private bool CheckIfPresetEmpty()
        {
            bool isEmpty = true;
            var savedCarSetup = PersistentStorage.GetObjectWithJsonUtility<CarSetup>(Global.Settings.CarSetting.GetCarSetup(Context.Player.CarPresetIndex).ID);
            
            return isEmpty; 
        }

        private void OnAutoVaultButton()
        {
            //Close();
        }


        protected override void OnDeinitialize()
        {
            _selectionDots.SelectionChanged -= OnSelectionChanged;


            // _nextCarButton.onClick.RemoveListener(OnNextCarButton);
            // _previousCarButton.onClick.RemoveListener(OnPreviousCarButton);
            //_selectButton.onClick.RemoveListener(OnSelectButton);
            _startRoomButton.onClick.RemoveListener(OnStartRoomButton);
            _nextButton.onClick.RemoveListener(OnNextButton);
            _settingButton.onClick.RemoveListener(OnSettingButton);
            _singleGameplayButton.onClick.RemoveListener(OnSinglePlayerButton);
            base.OnDeinitialize();
        }

        private void OnSinglePlayerButton()
        {
            
        }

        private void OnSelectionMapChanged(int obj)
        {
        }

        private void OnSettingButton()
        {
            Open<UISettingsView>();
        }

        private void OnNextButton()
        {
            //Context.UI.OpenWithBackView<UIMatchLobbyView>(this);
            Close();
        }

        protected override void OnOpen(params object[] cardInfo)
        {
            base.OnOpen(cardInfo);

            ProcessBasicFuntionality(true);

            this._gameMode = (GameMode)cardInfo[0];
            Context.Garage.ShowCar(Context.Player.CarPresetIndex, true);
            OnGarageOpen();
            OnInitializeSlots();
        }

        protected override void OnOpen()
        {
            base.OnOpen();

            ProcessBasicFuntionality(true);

            OnGarageOpen();
            OnInitializeSlots();
        }


        private void OnGarageOpen()
        {
            if (_uiPrepared == false)
            {
                _uiPrepared = true;
            }


            _previewCar = Context.Player.CarPresetIndex;


            _selectionDots.Refresh(Context.Settings.CarSetting.CarsSetup.Length, false);

            UpdateCar();


            switch (_gameMode)
            {
                case GameMode.Single:
                    //For Signal And Multiplayer
                    muliplayerPanel.SetActive(false);
                    singleplayerPanel.SetActive(true);
                    maxPlayer.SetActive(false);
                    maxLap.SetActive(true);

                    //For Presets
                    _autovaultButton.SetActive(false);
                    _presetsPanel.SetActive(false);

                    break;
                case GameMode.Multiplayer:
                    //For Signal And Multiplayer
                    muliplayerPanel.SetActive(true);
                    singleplayerPanel.SetActive(false);
                    maxPlayer.SetActive(true);
                    maxLap.SetActive(true);

                    //For Presets
                    _autovaultButton.SetActive(false);
                    _presetsPanel.SetActive(false);

                    // _match = Global.Services.Matchmaking.CurrentMatch;
                    //if (!Global.Services.Network.Client.InLobby)
                    //{
                    //    StartCoroutine(Connect_Coroutine());
                    //}

                    break;


                case GameMode.PresetMaking:
                    //For Signal And Multiplayer
                    muliplayerPanel.SetActive(false);
                    singleplayerPanel.SetActive(false);
                    maxPlayer.SetActive(false);
                    maxLap.SetActive(false);

                    //For Presets
                    _autovaultButton.SetActive(true);
                    _presetsPanel.SetActive(true);

                   
                    break;
            }


            //totalPlayer.text = Global.Settings.Match.MaxPlayers.ToString();
        }

        private void OnInitializeSlots()
        {
            foreach (var powerupSlot in powerupSlots)
            {
                powerupSlot.onClick.AddListener(() => OnPowerupSlotClicked(powerupSlot));
            }

            foreach (var techSlot in techSlots)
            {
                techSlot.onClick.AddListener(() => OnTechSlotClicked(techSlot));
            }
        }

        private void OnPowerupSlotClicked(UIButton slotButton)
        {
           
        }

        private void OnTechSlotClicked(UIButton slotButton)
        {
           
        }

        protected override void OnClose()
        {
            // Context.Garage.ShowCar(Context.Player.Car, true);

            ProcessBasicFuntionality(false);

            base.OnClose();
        }

        // PRIVATE METHODS

        private void ProcessBasicFuntionality(bool value)
        {
            if (bg)
                bg.SetActive(value);
            if (camera)
                camera.enabled = value;
        }

        private void OnStartRoomButton()
        {
            
        }

        private void CreateMatch()
        {
            
        }

        private void OnSelectButton()
        {
            ClearTempEquips();
            Global.PlayerService.CarSetupUpdated -= Global.Settings.CarSetting.GetCarSetup(Context.Player.CarPresetIndex).UpdateCarSetup;
            (Context.Player as PlayerData).CarPresetIndex = _previewCar;
            Global.PlayerService.CarSetupUpdated += Global.Settings.CarSetting.GetCarSetup(Context.Player.CarPresetIndex).UpdateCarSetup;
            UpdateCarPanel();
        }

        void ClearTempEquips()
        {
            var savedCarSetup = PersistentStorage.GetObjectWithJsonUtility<CarSetup>(Global.Settings.CarSetting.GetCarSetup(Context.Player.CarPresetIndex).ID); 
        }

        private void OnSelectionChanged(int index)
        {
            currentPreset = index;   
            _previewCar = Context.Settings.CarSetting.CarsSetup[index].ID;
            UpdateCar();
        }

        private void OnNextCarButton()
        {
            var carSetups = Context.Settings.CarSetting.CarsSetup;
            int count = carSetups.Length;

            int currentIndex = -1;
            for (int i = 0; i < carSetups.Length; i++)
            {
                if(carSetups[i].ID == _previewCar)
                {
                    currentIndex = i;
                }
            }
            int nextIndex = (currentIndex + 1) % count;

            _previewCar = carSetups[nextIndex].ID;
            UpdateCar();
        }

        private void OnPreviousCarButton()
        {
            var carSetups = Context.Settings.CarSetting.CarsSetup;
            int count = carSetups.Length;

            int currentIndex = -1;
            for (int i = 0; i < carSetups.Length; i++)
            {
                if (carSetups[i].ID == _previewCar)
                {
                    currentIndex = i;
                }
            }
            int previousIndex = (currentIndex - 1 + count) % count;

            _previewCar = carSetups[previousIndex].ID;
            UpdateCar();
        }

        private void UpdateCar()
        {
            var carSetups = Context.Settings.CarSetting.CarsSetup;
            int currentIndex = -1;
            for (int i = 0; i < carSetups.Length; i++)
            {
                if (carSetups[i].ID == _previewCar)
                {
                    currentIndex = i;
                }
            }
            _selectionDots.Selection = currentIndex;

            UpdateCarPreview();
        }


        private void UpdateCarPreview()
        {
            if (_previewCar.HasValue() == false)
            {
                Context.Garage.HideCar();
                return;
            }

            OnSelectButton();
            Context.Garage.ShowCar();
        }

        private void UpdateCarPanel()
        {
            var setup = Context.Settings.CarSetting.GetCarSetup(_previewCar);

            _carPanel.SetActive(setup != null);

            if (setup == null)
                return;

            _carPanel.SetData(setup);

            //bool isSelected = _previewCar == Context.Player.Car;
            // _selectButton.SetActive(isSelected == false);
            // _selectedGroup.SetActive(isSelected);
        }

        private bool OnNotificationReceived()
        {
            return false;
        }

    }
}