using UnityEngine;

namespace MoonKart
{
    public class RuntimeSettings
    {

        
        public const string KeyMusicVolume = "MusicVolume";
        public const string KeySoundVolume = "SoundVolume";
        public const string KeyGfxQuality = "GFXQuality";
        public const string KeyGraphicsQuality = "GraphicsQuality";

        public bool MusicVolume
        {
            get { return _options.GetBool(KeyMusicVolume); }
            set { _options.Set(KeyMusicVolume, value, false); }
        }

        public bool SoundVolume
        {
            get { return _options.GetBool(KeySoundVolume); }
            set { _options.Set(KeySoundVolume, value, false); }
        }
        
        public int GFXQuality
        {
            get { return _options.GetInt(KeyGfxQuality); }
            set { _options.Set(KeyGfxQuality, value, false); }
        }

        public int Graphics
        {
            get { return _options.GetInt(KeyGraphicsQuality); }
            set { _options.Set(KeyGraphicsQuality, value, false); }
        }

        // PRIVATE MEMBERS

        private Options _options = new Options();

        // PUBLIC METHODS

        public void Initialize(GlobalSettings settings)
        {
            _options.Initialize(settings.DefaultOptions, true, "Options.v2.");

            Graphics = QualitySettings.GetQualityLevel();

            _options.SaveChanges();
        }
    }
}