using UnityEngine;
using UnityEngine.UI;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace MoonKart.UI
{
	public class UIToggle : Toggle
	{
		// PRIVATE MEMBERS

		[SerializeField]
		private bool _playValueChangedSound = true;



		private UIWidget _parent;

		// MONOBEHAVIOR

		protected override void Awake()
		{
			base.Awake();

			// Toggle Awake is executed in Editor as well
			if (Application.isPlaying == true)
			{
				onValueChanged.AddListener(OnValueChanged);
			}
		}

		protected override void OnDestroy()
		{
			onValueChanged.RemoveListener(OnValueChanged);

			base.OnDestroy();
		}

		// PRIVATE METHODS

		private void OnValueChanged(bool isSelected)
		{
			if (_playValueChangedSound == false)
				return;

			if (isSelected == false && group != null && group.allowSwitchOff == false)
				return;

			if (_parent == null)
			{
				_parent = GetComponentInParent<UIWidget>();
			}

			if (_parent == null)
				return;


		}
	}
}
