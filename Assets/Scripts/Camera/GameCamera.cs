using Cinemachine;
using UnityEngine;

namespace MoonKart
{
	public class GameCamera : GameService 
	{
		// PRIVATE MEMBERS

		[SerializeField]
		private CinemachineVirtualCamera _playerCamera;
		[SerializeField]
		private CinemachineVirtualCamera _sceneCamera;
		
		public GameObject player { get; set; }

		// GameService INTERFACE

		protected override void OnInitialize()
		{
			base.OnInitialize();

			//if (Context.Entities != null)
			//{
			//	Context.Entities.LocalPlayerChanged += OnLocalPlayerChanged;
			//}
		}

		protected override void OnDeinitialize()
		{
			//if (Context.Entities != null)
			//{
			//	Context.Entities.LocalPlayerChanged -= OnLocalPlayerChanged;
			//}

			base.OnDeinitialize();
		}

		// PRIVATE METHODS

		public virtual void OnLocalPlayerChanged(GameObject player)
		{
			this.player = player;
			bool hasPlayer = player != null;

			if (hasPlayer == true)
			{
				_playerCamera.Follow = player.transform;
				_playerCamera.LookAt = player.transform;
			}

			_playerCamera.enabled = hasPlayer;
			_sceneCamera.enabled = hasPlayer == false;
		}
	}
}
