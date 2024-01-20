using UnityEngine;
using System;

namespace MoonKart
{
    [Serializable]
    [CreateAssetMenu(fileName = "GlobalSettings", menuName = "Settings/Global Settings")]
    public sealed class GlobalSettings : ScriptableObject
    {
        // PUBLIC MEMBERS

        public CarSettings CarSetting;
        
        public OptionsData DefaultOptions;

        public SceneSettings Scene;
        // PUBLIC METHODS

        public void UnloadAssets()
        {
            CarSetting.UnloadAssets();
        }
    }
}