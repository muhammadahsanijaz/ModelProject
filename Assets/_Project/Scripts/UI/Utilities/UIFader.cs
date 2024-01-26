using DG.Tweening;
using UnityEngine;

namespace MoonKart.UI
{
	[RequireComponent(typeof(CanvasGroup))]
	public class UIFader : UIBehaviour
	{
		// PRIVATE MEMBERS

		[SerializeField]
		private float _startDelay;
		[SerializeField]
		private EFadeDirection _direction = EFadeDirection.FadeIn;
		[SerializeField]
		private float _duration = 0.5f;
		[SerializeField]
		private Ease _ease = Ease.OutQuad;
		[SerializeField]
		private bool _resetOnDisable = true;

		private float _resetValue;
		private float _startValue;
		private float _targetValue;
		private float _time;
		private bool _isFinished;

		// MONOBEHAVIOR

		protected void OnEnable()
		{
			if (_isFinished == false)
			{
				_resetValue = CanvasGroup.alpha;
				_startValue = _direction == EFadeDirection.FadeIn ? 0f : 1f;
				_targetValue = _direction == EFadeDirection.FadeIn ? 1f : 0f;
				_time = -_startDelay;

				CanvasGroup.alpha = _startValue;
			}
		}

		protected void Update()
		{
			if (_isFinished == true)
				return;

			_time += Time.deltaTime;

			if (_time >= _duration)
			{
				_time = _duration;
				_isFinished = true;
			}

			float progress = DOVirtual.EasedValue(0f, 1f, _time / _duration, _ease);
			CanvasGroup.alpha = Mathf.Lerp(_startValue, _targetValue, progress);
		}

		protected void OnDisable()
		{
			if (_resetOnDisable == true)
			{
				CanvasGroup.alpha = _resetValue;
				_isFinished = false;
			}
		}

		// HELPERS

		public enum EFadeDirection
		{
			FadeIn,
			FadeOut,
		}
	}
}
