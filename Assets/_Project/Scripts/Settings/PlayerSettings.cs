using UnityEngine;
using System;
using System.Linq;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace MoonKart
{
	[Serializable]
	[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings/Player Settings")]
	public class PlayerSettings : ScriptableObject // 
	{
		// PUBLIC MEMBERS

		public string DefaultPlayer => _defaultPlayer;
		public PlayerSetup[] PlayersSetup => _players;

		// PRIVATE MEMBERS

		
		[SerializeField]
		private string _defaultPlayer;

	#if ODIN_INSPECTOR
		[SerializeField, ListDrawerSettings(Expanded = true)]
		private PlayerSetup[] _players;
	#else
		[SerializeField]
		private playersetup[] _players;
	#endif

		// PUBLIC METHODS

		public PlayerSetup GetSetup(string PlayerID)
		{
			if (PlayerID.HasValue() == false)
				return null;
            foreach (var playersetup in _players)
            {
				if(playersetup.ID == PlayerID)
                {
					return playersetup;
                }
            }

			return null;
		}
		public PlayerSetup GetSetup(GameObject Player)
		{
			if (Player == null)
				return null;
			foreach (var playersetup in _players)
			{
				if (playersetup.PlayerPrefab == Player)
				{
					return playersetup;
				}
			}

			return null;
		}

		public PlayerSetup GetRandomSetup()
		{
			return _players[UnityEngine.Random.Range(0, _players.Length)];
		}

		public void UnloadAssets()
		{
			for (int i = 0; i < _players.Length; i++)
			{
				_players[i].UnloadAssets();
			}
		}

    }

	[Serializable]
	public class PlayerSetup
	{

		// PRIVATE MEMBERS
		[SerializeField]
		private string _id;
		[NonSerialized] private GameObject _cachedPlayerPrefab;
		[NonSerialized] private string _playersetupName;
		[NonSerialized] private string _playersetupDescription;

		// PUBLIC MEMBERS

		public string ID => _id;
		public string DisplayName => _playersetupName;
		public string Description => _playersetupDescription;

		// PUBLIC METHODS

		//////////////// Player Prototype

		public GameObject PlayerPrefab
		{
			get
			{
                return _cachedPlayerPrefab;
			}
		}

		internal void SetNewPlayerSetup()
        {
			Saveplayersetup();
		}

		public void InitializePlayerSetup()
		{

		}


		public void UpdatePlayerSetup()
        {
			
        }


		internal void Saveplayersetup()
        {
			PersistentStorage.SetObjectWithJsonUtility(ID,this,true);
		}
        

        public void UnloadAssets()
		{
			_cachedPlayerPrefab = null;
		}

        internal void ResetSetup(PlayerSetup playersetup)
        {
		}
    }

	[Serializable]
	public enum EPlayerDifficulty
	{
		None,
		Easy,
		Normal,
		Hard,
	}
}
