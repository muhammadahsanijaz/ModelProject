using UnityEngine;
using System;
using System.Linq;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace MoonKart
{
	[Serializable]
	[CreateAssetMenu(fileName = "CarSettings", menuName = "Settings/Car Settings")]
	public class CarSettings : ScriptableObject // 
	{
		// PUBLIC MEMBERS

		public string DefaultCar => _defaultCar;
		public CarSetup[] CarsSetup => _cars;

		// PRIVATE MEMBERS

		
		[SerializeField]
		private string _defaultCar;

	#if ODIN_INSPECTOR
		[SerializeField, ListDrawerSettings(Expanded = true)]
		private CarSetup[] _cars;
	#else
		[SerializeField]
		private CarSetup[] _cars;
	#endif

		// PUBLIC METHODS

		public CarSetup GetCarSetup(string carID)
		{
			if (carID.HasValue() == false)
				return null;
            foreach (var carSetup in _cars)
            {
				if(carSetup.ID == carID)
                {
					return carSetup;
                }
            }

			return null;
		}
		public CarSetup GetCarSetup(GameObject car)
		{
			if (car == null)
				return null;
			foreach (var carSetup in _cars)
			{
				if (carSetup.CarPrefab == car)
				{
					return carSetup;
				}
			}

			return null;
		}

		public CarSetup GetRandomCarSetup()
		{
			return _cars[UnityEngine.Random.Range(0, _cars.Length)];
		}

		public void UnloadAssets()
		{
			for (int i = 0; i < _cars.Length; i++)
			{
				_cars[i].UnloadAssets();
			}
		}

    }

	[Serializable]
	public class CarSetup
	{

		// PRIVATE MEMBERS
		[SerializeField]
		private string _id;
		[NonSerialized] private GameObject _cachedCarPrefab;
		[NonSerialized] private GameObject _cachedDriverPrefab;
		[NonSerialized] private string _carSetupName;
		[NonSerialized] private string _carSetupDescription;

		// PUBLIC MEMBERS

		public string ID => _id;
		public string DisplayName => _carSetupName;
		public string Description => _carSetupDescription;

		// PUBLIC METHODS

		//////////////// Car Prototype

		public GameObject CarPrefab
		{
			get
			{
                return _cachedCarPrefab;
			}
		}


		public GameObject DriverPrefab
		{
			get
			{
				
				return _cachedDriverPrefab;
			}
		}

		internal void SetNewCarSetup()
        {
			SaveCarSetup();
		}

		public void InitializeCarSetup()
		{

		}


		public void UpdateCarSetup()
        {
			
        }


		internal void SaveCarSetup()
        {
			PersistentStorage.SetObjectWithJsonUtility(ID,this,true);
		}
        

        public void UnloadAssets()
		{
			_cachedCarPrefab = null;
		}

        internal void ResetSetup(CarSetup carSetup)
        {
		}
    }

	[Serializable]
	public enum ECarDifficulty
	{
		None,
		Easy,
		Normal,
		Hard,
	}
}
