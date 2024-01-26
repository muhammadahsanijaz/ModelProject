using UnityEngine;
using System;

namespace MoonKart
{
    [Serializable]
    [CreateAssetMenu(fileName = "GlobalSettings", menuName = "Settings/Global Settings")]
    public sealed class GlobalSettings : ScriptableObject
    {
        // PUBLIC MEMBERS

        public PlayerSettings PlayerSetting;
        
        public OptionsData DefaultOptions;

        public SceneSettings Scene;
        // PUBLIC METHODS

        public void UnloadAssets()
        {
            PlayerSetting.UnloadAssets();
        }
    }
}