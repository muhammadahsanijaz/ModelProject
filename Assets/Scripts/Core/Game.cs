using UnityEngine;
using System.Collections.Generic;
using MoonKart.UI;

namespace MoonKart
{
    public class GameContext
    {
        public IPlayer Player;
        public GlobalSettings Settings;
        public RuntimeSettings RuntimeSettings;

        public GameUI UI;
        public GameAudio       Audio;
        public GameCache Cache;
        public GameCamera Camera;
        public GameInput Input;

        // Gameplay
        public int LocalPlayer;

        // Menu
        public Garage Garage;
    }

    public class Game : MonoBehaviour
    {
        // PUBLIC MEMBERS

        public bool IsActive { get; private set; }

        // PROTECTED MEMBERS

        protected GameContext _context;
        protected List<GameService> _allServices;

        // PRIVATE MEMBERS
        [SerializeField] private GameAudio _gameAudio;
        [SerializeField] private GameCache _gameCache;
        [SerializeField] private GameCamera _gameCamera;
        [SerializeField] private GameInput _gameInput;

        private bool _isInitialized;

        // PUBLIC METHODS

        public void Quit()
        {
            Deinitialize();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
        }

        public void Deinitialize()
        {
            if (_isInitialized == false)
                return;

            OnDeactivate();

            IsActive = false;

            OnDeinitialize();
            DeinitializeAll();

            _isInitialized = false;
        }

        // MONOBEHAVIOR

        protected void Awake()
        {
            CheckVersion();

            _context = PrepareContext();

            _allServices = new List<GameService>(16);
            AddServices(_allServices);

            OnInitialize();

            _isInitialized = true;
        }

        protected void Start()
        {
            if (_context.UI == null && this is Gameplay)
            {
                // Standalone gameplay scene Start, load UI scene
                //yield return SceneManager.LoadSceneAsync(_context.Settings.Scene.GameplayUIScene, LoadSceneMode.Additive);
                _context.UI = FindObjectOfType<GameUI>();
            }

            // UI cannot be initialized in Awake, Canvas elements need to Awake first
            _context.UI.Initialize(this, _context);
            _allServices.Add(_context.UI);

            OnActivate();

            IsActive = true;
        }

        protected virtual void Update()
        {
            if (IsActive == false)
                return;

            OnTick();
        }

        protected void LateUpdate()
        {
            if (IsActive == false)
                return;

            OnLateTick();
        }

        protected void OnDestroy()
        {
            Deinitialize();
        }

        protected void OnApplicationQuit()
        {
            Deinitialize();
        }

        // PROTECTED METHODS

        private void CheckVersion()
        {
            if (this is Menu) {
                string currentVersion = PlayerPrefs.GetString("version", "");
                if (currentVersion != Application.version)
                {
                    currentVersion = Application.version.ToString();
                  //  Global.Settings.CardsLibrary.ResetAllCardStatestAll();
                    PlayerPrefs.SetString("version", currentVersion);
                }

            }
        }

        protected virtual GameContext PrepareContext()
        {
            return new GameContext
            {
                LocalPlayer = -1,
                UI = FindObjectOfType<GameUI>(),
                Cache = _gameCache,
                Camera = _gameCamera,
                Input = _gameInput,
                Audio = _gameAudio,
                Player = Global.PlayerService.PlayerData,
                Settings = Global.Settings,
                RuntimeSettings = Global.RuntimeSettings,
            };
        }

        protected virtual void AddServices(List<GameService> services)
        {
            services.Add(_gameCache);
            services.Add(_gameCamera);
            services.Add(_gameInput);
        }

        protected virtual void OnInitialize()
        {
            for (int i = 0; i < _allServices.Count; i++)
            {
                _allServices[i].Initialize(this, _context);
            }
        }

        protected virtual void OnActivate()
        {
            for (int i = 0; i < _allServices.Count; i++)
            {
                _allServices[i].Activate();
            }
        }

        protected virtual void OnTick()
        {
            for (int i = 0; i < _allServices.Count; i++)
            {
                _allServices[i].Tick();
            }
        }

        protected virtual void OnLateTick()
        {
            for (int i = 0; i < _allServices.Count; i++)
            {
                _allServices[i].LateTick();
            }
        }

        protected virtual void OnDeactivate()
        {
            for (int i = _allServices.Count - 1; i >= 0; i--)
            {
                var service = _allServices[i];

                if (service != null)
                {
                    service.Deactivate();
                }
            }
        }

        protected virtual void OnDeinitialize()
        {
        }

        // PRIVATE METHODS

        private void DeinitializeAll()
        {
            for (int i = _allServices.Count - 1; i >= 0; i--)
            {
                var service = _allServices[i];

                if (service != null)
                {
                    service.Deinitialize();
                }
            }

            _allServices.Clear();

        }
    }
}