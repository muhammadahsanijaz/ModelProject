using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MoonKart.UI
{
	public class GameplayUI : GameUI 
	{
		// PRIVATE MEMBERS

		public UIView ReconnectingView => _reconnectingViews;
		public UIView RemoveFromMatchViews => _RemoveFromMatchViews;
		// PRIVATE MEMBERS

		[SerializeField]
		private DefaultViewsSetup[] _defaultViews;

		private DefaultViewsSetup _currentDefaultViews;
		
		[SerializeField]
		private UIView _reconnectingViews;
		[SerializeField]
		private UIView _RemoveFromMatchViews;
		
		

		// GameUI INTERFACE

		protected override void OnInitializeInternal()
		{
			base.OnInitializeInternal();
			_reconnectingViews.SetActive(false);
			_RemoveFromMatchViews.SetActive(false);
			
		}

		protected override void OnTickInternal()
		{
			base.OnTickInternal();
		}

		protected override void OnGameSetInternal()
		{
			base.OnGameSetInternal();
			//Context.Matchmaking.RemoveFromMatch += RemoveFromMatch;
			// if (Global.Services?.Matchmaking?.CurrentMatch == null)
			// {
			// 	Global.Services.Matchmaking.CurrentMatch.Disconnected += OnDisConnect;
			// 	Global.Services.Matchmaking.CurrentMatch.Reconnected += OnReconnect;
			// 	Global.Services.Matchmaking.CurrentMatch.NetworkChanged += OnReconnectNwtworkChanged;
			// }
		}

		protected override void OnGameClearedInternal()
		{
			base.OnGameClearedInternal();

			//Global.Services.Matchmaking.CurrentMatch.Disconnected -= OnDisConnect;
			//Global.Services.Matchmaking.CurrentMatch.Reconnected -= OnReconnect;
			//Global.Services.Matchmaking.CurrentMatch.NetworkChanged -= OnReconnectNwtworkChanged;

		}

		// PRIVATE METHODS


		private void SetDefaultViews(DefaultViewsSetup setup)
		{
			if (_currentDefaultViews != null)
			{
				for (int i = 0; i < _currentDefaultViews.Views.Length; i++)
				{
					UIView view = _currentDefaultViews.Views[i];

					if (setup != null && setup.Views.Contains(view) == true)
						continue;

					view.Close();
				}
			}

			_currentDefaultViews = setup;

			if (setup == null)
				return;

			for (int i = 0; i < setup.Views.Length; i++)
			{
				setup.Views[i].Open();
			}
		}



		// CLASSES

		[Serializable]
		private class DefaultViewsSetup
		{
			public UIView[] Views;
		}
	}
}
