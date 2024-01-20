using UnityEngine;
using System.Collections.Generic;

namespace MoonKart
{
	public interface IBackHandler
	{
		int Priority { get; }
		bool IsActive { get; }
		bool OnBackAction();
	}

	public class GameInput : GameService 
	{
		// PRIVATE MEMBERS

		private List<IBackHandler> _backHandlers = new List<IBackHandler>(32);

		// GameService INTERFACE

		protected override void OnTick()
		{
			base.OnTick();

			if (UnityEngine.Input.GetButtonDown("Cancel") == true)
			{
				BackAction();
			}
		}

		// PRIVATE METHODS

		private void BackAction()
		{
			if (_backHandlers.Count == 0)
			{
				Context.UI.GetAll(_backHandlers);
				_backHandlers.Add(Context.UI);

				_backHandlers.Sort((a, b) => b.Priority.CompareTo(a.Priority));
			}

			for (int i = 0, count = _backHandlers.Count; i < count; ++i)
			{
				var handler = _backHandlers[i];
				if (handler.IsActive == true && handler.OnBackAction() == true)
					break;
			}
		}
	}
}
