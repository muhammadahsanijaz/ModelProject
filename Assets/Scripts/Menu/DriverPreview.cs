using UnityEngine;

namespace MoonKart
{
	public class DriverPreview : CoreBehaviour 
	{
		// PUBLIC MEMBERS

		public string DriverID => _DriverID;

		// PRIVATE MEMBERS

		[SerializeField]
		private Transform _DriverParent;

		private string _DriverID;
		private GameObject _DriverInstance;

		// PUBLIC METHODS

		public void ShowDriver(string DriverID, bool force = false)
		{
			if (DriverID == _DriverID && force == false)
				return;

			ClearDriver();
			InstantiateDriver(DriverID);
		}

		public void HideDriver()
		{
			ClearDriver();
		}

		// PRIVATE METHODS

		private void InstantiateDriver(string DriverID)
		{
			if (DriverID.HasValue() == false)
				return;

			var DriverSetup = Global.Settings.CarSetting.GetCarSetup(DriverID);

			if (DriverSetup == null)
				return;

			_DriverID = DriverID;
			_DriverInstance = Instantiate(DriverSetup.DriverPrefab, _DriverParent);
			_DriverInstance.GetComponent<Animator>().SetTrigger("MainMenu");
		}

		private void ClearDriver()
		{
			_DriverID = null;

			if (_DriverInstance == null)
				return;

			Destroy(_DriverInstance);
			_DriverInstance = null;
		}
	}
}
