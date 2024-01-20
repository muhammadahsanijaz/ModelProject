namespace MoonKart
{
	public class GameService : CoreBehaviour
	{
		// PUBLIC MEMBERS

		public GameContext Context => _context; 
		public Game        Game => _game;
		public bool        QuantumGameSet => _lastGameSet;

		// PRIVATE MEMBERS

		private Game _game;
		private GameContext _context;
		private bool _isInitialized;
		private bool _isActive;

		private bool _lastGameSet;

		// INTERNAL METHODS

		
		
		internal void Initialize(Game game, GameContext context)
		{
			if (_isInitialized == true)
				return;

			_game = game;
			_context = context;

			OnInitialize();

			_isInitialized = true;

			GameSet();
		}

		internal void Activate()
		{
			if (_isInitialized == false)
				return;

			if (_isActive == true)
				return;

			OnActivate();

			_isActive = true;
		}

		internal void Tick()
		{
			if (_isActive == false)
				return;

			OnTick();
		}

		internal void LateTick()
		{
			if (_isActive == false)
				return;

			OnLateTick();
		}

		internal void Deactivate()
		{
			if (_isActive == false)
				return;

			OnDeactivate();

			_isActive = false;
		}

		internal void Deinitialize()
		{
			if (_isInitialized == false)
				return;

			Deactivate();

			ClearLastGame();

			OnDeinitialize();

			_context = null;
			_game = null;
			_isInitialized = false;
		}

		internal void GameSet(bool force = false)
		{
			if (_isInitialized == false)
				return;

			ClearLastGame();
		}

		internal void GameCleared()
		{
			if (_isInitialized == false)
				return;

			ClearLastGame();
		}

		// GameService INTERFACE

		protected virtual void OnInitialize()
		{
		}

		protected virtual void OnDeinitialize()
		{
		}

		protected virtual void OnActivate()
		{
		}

		protected virtual void OnDeactivate()
		{
		}

		protected virtual void OnTick()
		{
		}

		protected virtual void OnLateTick()
		{
		}

		protected virtual void OnGameSet()
		{
		}

		protected virtual void OnGameCleared()
		{
		}

		// PRIVATE METHODS

		private void ClearLastGame()
		{
			if (_lastGameSet == false)
				return;

			OnGameCleared();

			_lastGameSet = false;
		}
	} 
}
