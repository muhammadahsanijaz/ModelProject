using UnityEngine;

namespace MoonKart
{
	public class CarPreview : CoreBehaviour 
	{
		// PUBLIC MEMBERS

		public string CarID => _carID;

		// PRIVATE MEMBERS

		[SerializeField]
		private Transform _carParent;

		private string _carID;
		private GameObject _carInstance;

		// PUBLIC METHODS

		public void ShowCar(string carID, bool force = false)
		{
			if (carID == _carID && force == false)
				return;

			ClearCar();
			InstantiateCar(carID);
		}

		public void HideCar()
		{
			ClearCar();
		}

		// PRIVATE METHODS

		private void InstantiateCar(string carID)
		{
			if (carID.HasValue() == false)
				return;

			var carSetup = Global.Settings.CarSetting.GetCarSetup(carID);

			if (carSetup == null)
				return;

			_carID = carID;
			_carInstance = Instantiate(carSetup.CarPrefab, _carParent);
		}

		private void ClearCar()
		{
			_carID = null;

			if (_carInstance == null)
				return;

			Destroy(_carInstance);
			_carInstance = null;
		}
	}
}
