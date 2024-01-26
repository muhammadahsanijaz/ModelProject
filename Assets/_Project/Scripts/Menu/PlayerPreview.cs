using UnityEngine;

namespace MoonKart
{
	public class PlayerPreview : CoreBehaviour 
	{
		// PUBLIC MEMBERS

		public string PlayerID => _playerID;

		// PRIVATE MEMBERS

		[SerializeField]
		private Transform _playerParent;

		private string _playerID;
		private GameObject _playerInstance;

		// PUBLIC METHODS

		public void ShowPlayer(string playerID, bool force = false)
		{
			if (playerID == _playerID && force == false)
				return;

			ClearPlayer();
			InstantiatePlayer(playerID);
		}

		public void HidePlayer()
		{
			ClearPlayer();
		}

		// PRIVATE METHODS

		private void InstantiatePlayer(string playerID)
		{
			if (playerID.HasValue() == false)
				return;

			var playerSetup = Global.Settings.PlayerSetting.GetSetup(playerID);

			if (playerSetup == null)
				return;

			_playerID = playerID;
			_playerInstance = Instantiate(playerSetup.PlayerPrefab, _playerParent);
		}

		private void ClearPlayer()
		{
			_playerID = null;

			if (_playerInstance == null)
				return;

			Destroy(_playerInstance);
			_playerInstance = null;
		}
	}
}
