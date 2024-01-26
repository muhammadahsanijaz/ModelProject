using UnityEngine;

namespace MoonKart.UI
{
    public class UISettingsView : UICloseView
    {
        // UICloseView INTERFACE

        [SerializeField] private UIToggle musicToggle;
        [SerializeField] private UIToggle soundToggle;
        // [SerializeField] private UIToggle gfxQualityEasyToggle;
        // [SerializeField] private UIToggle gfxQualityMediumToggle;
        // [SerializeField] private UIToggle gfxQualityHardToggle;
        [SerializeField] private UIToggle graphicsQualityEasyToggle;
        [SerializeField] private UIToggle graphicsQualityMediumToggle;
        [SerializeField] private UIToggle graphicsQualityHardToggle;
        [SerializeField] private UIButton confirmButton;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            musicToggle.onValueChanged.AddListener(OnMusicToggle);
            soundToggle.onValueChanged.AddListener(OnSoundToggle);
            // gfxQualityEasyToggle.onValueChanged.AddListener(OnGfxQualityEasyToggle);
            // gfxQualityMediumToggle.onValueChanged.AddListener(OnGfxQualityMediumToggle);
            // gfxQualityHardToggle.onValueChanged.AddListener(OnGfxQualityHardToggle);
            graphicsQualityEasyToggle.onValueChanged.AddListener(OnGraphicsQualityEasyToggle);
            graphicsQualityMediumToggle.onValueChanged.AddListener(OnGraphicsQualityMediumToggle);
            graphicsQualityHardToggle.onValueChanged.AddListener(OnGraphicsQualityHardToggle);
            confirmButton.onClick.AddListener(OnConfirmButton);
            
        }

       

        protected override void OnDeinitialize()
        {
            musicToggle.onValueChanged.RemoveListener(OnMusicToggle);
            soundToggle.onValueChanged.RemoveListener(OnSoundToggle);
            // gfxQualityEasyToggle.onValueChanged.RemoveListener(OnGfxQualityEasyToggle);
            // gfxQualityMediumToggle.onValueChanged.RemoveListener(OnGfxQualityMediumToggle);
            // gfxQualityHardToggle.onValueChanged.RemoveListener(OnGfxQualityHardToggle);
            graphicsQualityEasyToggle.onValueChanged.RemoveListener(OnGraphicsQualityEasyToggle);
            graphicsQualityMediumToggle.onValueChanged.RemoveListener(OnGraphicsQualityMediumToggle);
            graphicsQualityHardToggle.onValueChanged.RemoveListener(OnGraphicsQualityHardToggle);
            confirmButton.onClick.RemoveListener(OnConfirmButton);
            
            base.OnDeinitialize();
        }

        private void OnConfirmButton()
        {
           
           // Context.RuntimeSettings.Options.SaveChanges();

            var runtimeSettings = Context.RuntimeSettings;
            
            QualitySettings.SetQualityLevel(runtimeSettings.Graphics);

            Close();
        }
        protected override void OnOpen()
        {
            base.OnOpen();
            LoadValues();
        }

        protected override void OnClose()
        {
            base.OnClose();
            
           // Context.RuntimeSettings.Options.DiscardChanges();
            Context.Audio.UpdateVolume();
        }

        protected override void OnTick()
        {
            base.OnTick();
           // confirmButton.interactable = Context.RuntimeSettings.Options.HasUnsavedChanges;
        }
        
        private void LoadValues()
        {
            var runtimeSettings = Context.RuntimeSettings;

            musicToggle.isOn = (Global.RuntimeSettings.MusicVolume);
            soundToggle.isOn = (Global.RuntimeSettings.SoundVolume);

        
            
            //switch (runtimeSettings.Options.GetInt(RuntimeSettings.KeyGraphicsQuality))
            //{
            //    case 0:
            //        graphicsQualityEasyToggle.SetIsOnWithoutNotify(true);
            //        break;
            //    case 1:
            //        graphicsQualityMediumToggle.SetIsOnWithoutNotify(true);
            //        break;
            //    case 2:
            //        graphicsQualityHardToggle.SetIsOnWithoutNotify(true);
            //        break;
            //}

        }

        private void OnGraphicsQualityEasyToggle(bool value)
        {
            if(!value)
                return;
            Context.RuntimeSettings.Graphics = 0;
        }
        
        private void OnGraphicsQualityMediumToggle(bool value)
        {
            if(!value)
                return;
            Context.RuntimeSettings.Graphics = 1;
        }
     
        private void OnGraphicsQualityHardToggle(bool value)
        {
            if(!value)
                return;
            Context.RuntimeSettings.Graphics = 2;
        }

        private void OnSoundToggle(bool value)
        {
            Context.RuntimeSettings.SoundVolume = value;
            Context.Audio.UpdateVolume();
        }

        private void OnMusicToggle(bool value)
        {
            Context.RuntimeSettings.MusicVolume = value;

            Context.Audio.UpdateVolume();
        }


    }
}