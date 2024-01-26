using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Linq;

namespace MoonKart.UI
{
    public class MenuUI : GameUI
    {
        // PRIVATE MEMBERS

        [SerializeField] private UIView[] _defaultViews;

        
        // PUBLIC METHODS

        public void ResetViews()
        {
            for (int i = 0; i < _views.Length; i++)
            {
                var view = _views[i];

                if (view.IsOpen == false)
                    continue;

                if (_defaultViews.Contains(view) == true)
                    continue;

                view.Close();
            }

            OpenDefaultViews();
        }

        // GameUI INTEFACE

        protected override void OnInitializeInternal()
        {
            base.OnInitializeInternal();
            SetNewCarSetups();
            OpenDefaultViews();

            if (Context.Player.Nickname.HasValue() == false)
            {
                var changeNicknameView = Open<UIChangeNicknameView>();
                changeNicknameView.SetData("ENTER NICKNAME", true);
            }
        }

        // PRIVATE METHODS

        private void OpenDefaultViews()
        {
            for (int i = 0; i < _defaultViews.Length; i++)
            {
                _defaultViews[i].Open();
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            Global.PlayerService.CarSetupUpdated += Global.Settings.PlayerSetting.GetSetup(Context.Player.SelectedIndex).UpdatePlayerSetup;
            //Context.Garage.ShowCar(Context.Player.CarPresetIndex, true);

			
        }

        private void SetNewCarSetups()
        {
            foreach (var carSetup in Global.Settings.PlayerSetting.PlayersSetup)
            {
                var savedCarSetup = PersistentStorage.GetObjectWithJsonUtility<PlayerSetup>(carSetup.ID);
                if (savedCarSetup == null)
                {
                    carSetup.SetNewPlayerSetup();
                }
                else
                {
                    carSetup.InitializePlayerSetup();
                }
            }
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
        }

    }
}