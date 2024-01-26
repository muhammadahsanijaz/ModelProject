using UnityEngine;

namespace MoonKart
{
	public sealed class TimeScaler : MonoBehaviour 
	{
#if UNITY_EDITOR
		// PRIVATE MEMBERS

		[SerializeField]
		private float _initialTimeScale = 1f;

		// MONOBEHAVIOR

		private void Start()
		{
			Time.timeScale = _initialTimeScale;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.KeypadPlus) == true)
			{
				Time.timeScale += 0.1f;
			}
			else if (Input.GetKeyDown(KeyCode.KeypadMinus) == true)
			{
				Time.timeScale = Time.timeScale > 0.1f ? Time.timeScale - 0.1f : 0f;
			}
			else if (Input.GetKeyDown(KeyCode.KeypadEnter) == true)
			{
				Time.timeScale = 1f;
			}
		}
#endif
	}
}
