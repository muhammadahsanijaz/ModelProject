using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MoonKart
{
	public class PlayerService : IGlobalService
	{
		// PUBLIC MEMBERS

		public Action<PlayerData> PlayerDataChanged;
		public Action CarSetupUpdated;

		public PlayerData PlayerData { get; private set; }

		// IGlobalService INTERFACE


		// Load Player Data at Start
		void IGlobalService.Initialize()
		{
			PlayerData = LoadPlayerData();
		}

		//Check Every Tick Is Player Value Changes or Not
		void IGlobalService.Tick()
		{
			if (PlayerData.IsDirty == true)
			{
				SavePlayerData();
				PlayerDataChanged?.Invoke(PlayerData); // Invoke Event if Player Change Data
				PlayerData.ClearDirty();
			}
		}

		
		// Save Player Data On Game End
		void IGlobalService.Deinitialize()
		{
			SavePlayerData();

			PlayerDataChanged = null;
		}

		// PRIVATE METHODS

		private PlayerData LoadPlayerData()
		{

			PlayerData playerData = PersistentStorage.GetObjectWithJsonUtility<PlayerData>(GameConstants.PlayerInfo);
			if (playerData == null)
			{
				playerData = new PlayerData(Guid.NewGuid().ToString());
				playerData.CarPresetIndex = Global.Settings.CarSetting.DefaultCar;
				playerData.isReady = false;
			}

			return playerData;
		}

		private void SavePlayerData()
		{
			PersistentStorage.SetObjectWithJsonUtility(GameConstants.PlayerInfo, PlayerData, true);
		}
	}
}
