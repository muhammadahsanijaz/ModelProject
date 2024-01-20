using UnityEngine;
using System.Collections.Generic;

namespace MoonKart
{
	public class Gameplay : Game 
	{
		// PRIVATE MEMBERS


		// PUBLIC METHODS

		public void LoadMainMenu()
		{
			
			//Global.Loader.LoadScene(_context.Settings.Scene.MainMenuScene);
		}

		// Game INTERFACE

		protected override GameContext PrepareContext()
		{
			var context = base.PrepareContext();

			return context;
		}

		protected override void AddServices(List<GameService> services)
		{
			base.AddServices(services);
		}

		protected override void OnInitialize()
		{
			base.OnInitialize();
		}

		protected override void OnDeinitialize()
		{
			base.OnDeinitialize();
		}

		protected override void OnActivate()
		{
			base.OnActivate();

		}

		protected override void OnTick()
		{
			// Override base tick to tick services in exact order
			_context.UI.Tick();
			_context.Input.Tick();
			_context.Cache.Tick();
		}

		// PRIVATE METHODS

		

		private void StartGame()
		{
			for (int i = 0; i < _allServices.Count; i++)
			{
				_allServices[i].GameSet();
			}
		}

		private void OnGameDestroyed()
		{
			for (int i = _allServices.Count - 1; i >= 0; i--)
			{
				var service = _allServices[i];

				if (service != null)
				{
					service.GameCleared();
				}
			}
		}
	}
}
