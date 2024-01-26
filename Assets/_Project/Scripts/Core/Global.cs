using UnityEngine;
using UnityEngine.PlayerLoop;
using System.Collections.Generic;

namespace MoonKart
{
    public interface IGlobalService
    {
        void Initialize();
        void Tick();
        void Deinitialize();
    }

    /// <summary>
    /// Global Class which have Data of all Game
    /// </summary>
    public static class Global
    {
        // PUBLIC MEMBERS

        /// <summary>
        /// Contain Quantum Data - (1) Runtime Config  - (2) Player Config - (3) Quantum Runner  
        /// </summary>

        /// <summary>
        /// Contain Photon Game Services - (1) MatchMaKING  - (2) Lobby - (3) Logs  
        /// </summary>

        /// <summary>
        /// Getting Data From Scriptable we made for our game  
        /// </summary>
        public static GlobalSettings Settings { get; private set; }

        /// <summary>
        ///Game audio Setting, and Region
        /// </summary>
        public static RuntimeSettings RuntimeSettings { get; private set; }

        /// <summary>
        /// Scene Loader
        /// </summary>
        public static Loader Loader { get; private set; }
        
        // /// <summary>
        // /// Scene Loader
        // /// </summary>
        // public static GameData Loader { get; private set; }

        /// <summary>
        /// Player Data
        /// </summary>
        public static PlayerService PlayerService { get; private set; }

        // PRIVATE MEMBERS

        private static bool _isInitialized;

        private static List<IGlobalService> _globalServices = new List<IGlobalService>(16);

        // PUBLIC METHODS

        public static void Quit()
        {
            Deinitialize();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
        }

        // PRIVATE METHODS

        /// <summary>
        /// Callback invoked when starting up the runtime. Called before the first scene is loaded.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void InitializeSubSystem()
        {
#if UNITY_EDITOR
            if (Application.isPlaying == false)
                return;
#endif
            // PlayerLoopUtility contains functions for interacting with the player loop in the core of Unity. You can use this class to get the update order of all
            // native systems and set a custom order with new script entry points inserted.

            if (PlayerLoopUtility.HasPlayerLoopSystem(typeof(Global)) == false)
            {
                //PlayerLoopUtility.AddPlayerLoopSystem(typeof(Global), typeof(EarlyUpdate), EarlyUpdate, 0);
                //PlayerLoopUtility.AddPlayerLoopSystem(typeof(Global), typeof(FixedUpdate.ScriptRunBehaviourFixedUpdate),  BeforeFixedUpdate, AfterFixedUpdate);
                PlayerLoopUtility.AddPlayerLoopSystem(typeof(Global), typeof(Update.ScriptRunBehaviourUpdate), BeforeUpdate, AfterUpdate);
                //PlayerLoopUtility.AddPlayerLoopSystem(typeof(Global), typeof(PreLateUpdate.ScriptRunBehaviourLateUpdate), BeforeLateUpdate,  AfterLateUpdate);
                //PlayerLoopUtility.AddPlayerLoopSystem(typeof(Global), typeof(PostLateUpdate), PostLateUpdateFirst, 0);
                //PlayerLoopUtility.AddPlayerLoopSystem(typeof(Global), typeof(PostLateUpdate), PostLateUpdateLast);
            }

            Application.quitting -= OnApplicationQuit;
            Application.quitting += OnApplicationQuit;

            _isInitialized = true;
        }

        /// <summary>
        ///Callback invoked when the first scene's objects are loaded into memory but before Awake has been called.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeBeforeSceneLoad()
        {
            Initialize();

            BeforeSceneLoad();
        }

        /// <summary>
        ///Callback invoked when the first scene's objects are loaded into memory and after Awake has been called.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitializeAfterSceneLoad()
        {
            AfterSceneLoad();
        }

        /// <summary>
        /// Initialize Global
        /// </summary>
        private static void Initialize()
        {
            if (_isInitialized == false)
                return;

            GlobalSettings[] globalSettings = Resources.LoadAll<GlobalSettings>(""); // All Setting From resources
            Settings = globalSettings.Length > 0 ? Object.Instantiate(globalSettings[0]) : null;

            RuntimeSettings = new RuntimeSettings();
            RuntimeSettings.Initialize(Settings);

            PrepareGlobalServices();

            Loader = CreateStaticObject<Loader>();
            // Add Scene In Loader
            Loader.LoadingSceneName = Settings.Scene.LoadingScene;
            // Loader.SigneGameplaySceneName = Settings.Scene.GameplayScene;

            Loader.UnloadResources += OnUnloadResources;

            _isInitialized = true;
        }

        /// <summary>
        /// Deinitialize Global
        /// </summary>
        private static void Deinitialize()
        {
            if (_isInitialized == false)
                return;

            for (int i = _globalServices.Count - 1; i >= 0; i--)
            {
                var service = _globalServices[i];
                if (service != null)
                {
                    service.Deinitialize();
                }
            }


            PlayerLoopUtility.RemovePlayerLoopSystems(typeof(Game));

            OnUnloadResources();

            _isInitialized = false;
        }

        private static void BeforeSceneLoad()
        {

        }

        private static void AfterSceneLoad()
        {

        }

        private static void OnApplicationQuit()
        {
            Deinitialize();
        }

        private static void BeforeUpdate()
        {

            for (int i = 0; i < _globalServices.Count; i++)
            {
                _globalServices[i].Tick();
            }
        }

        private static void AfterUpdate()
        {
        }

        private static void PrepareGlobalServices()
        {
            PlayerService = new PlayerService();

            _globalServices.Add(PlayerService);

            for (int i = 0; i < _globalServices.Count; i++)
            {
                _globalServices[i].Initialize();
            }
        }

        private static void OnUnloadResources()
        {
            Settings.UnloadAssets();
        }

        private static T CreateStaticObject<T>() where T : Component
        {
            GameObject gameObject = new GameObject(typeof(T).Name);
            Object.DontDestroyOnLoad(gameObject);

            return gameObject.AddComponent<T>();
        }
    }
}