using System;
using UnityEngine;
using DG.Tweening;

namespace MoonKart
{
	public class SimpleAnimation : CoreBehaviour 
	{
		// PRIVATE MEMBERS

		[SerializeField]
		private float _startDelay;
		[SerializeField]
		private Animation _translation;
		[SerializeField]
		private Animation _rotation;
		[SerializeField]
		private Animation _scale;

		[Space]
		[SerializeField]
		private bool _playAutomatically = true;
		[SerializeField]
		private bool _resetOnStop = true;

		private float _delay;

		// PUBLIC METHODS

		public void Play()
		{
			if (_translation.HasStarted == true || _rotation.HasStarted == true || _scale.HasStarted == true)
				return;

			_translation.TryStart(transform.localPosition);
			_rotation.TryStart(transform.rotation.eulerAngles);
			_scale.TryStart(transform.localScale);

			_delay = _startDelay;
		}

		public void Stop()
		{
			if (_resetOnStop == true)
			{
				if (_translation.HasStarted == true)
				{
					transform.localPosition = _translation.RestartValue;
				}

				if (_rotation.HasStarted == true)
				{
					transform.localRotation = Quaternion.Euler(_rotation.RestartValue);
				}

				if (_scale.HasStarted == true)
				{
					transform.localScale = _scale.RestartValue;
				}
			}

			_translation.Stop();
			_rotation.Stop();
			_scale.Stop();
		}

		// MONOBEHAVIOR

		protected void OnEnable()
		{
			if (_playAutomatically == true)
			{
				Play();
			}
		}

		protected void Update()
		{
			_delay -= Time.deltaTime;
			if (_delay > 0)
				return;

			if (_translation.CanUpdate == true)
			{
				transform.localPosition = _translation.Update();
			}

			if (_rotation.CanUpdate == true)
			{
				transform.localRotation = Quaternion.Euler(_rotation.Update());
			}

			if (_scale.CanUpdate == true)
			{
				transform.localScale = _scale.Update();
			}
		}

		protected void OnDisable()
		{
			Stop();
		}

		// HELPERS

		[Serializable]
		public class Animation
		{
			public Vector3       Value;
			public Ease          Ease = Ease.Linear;
			public float         Duration;
			public float         Delay;
			public EPlayBehavior Behavior = EPlayBehavior.Restart;
			public bool          ValueIsAbsolute;

			public bool          HasStarted => _hasStarted;
			public bool          CanUpdate => _hasStarted == true && _isFinished == false;
			public Vector3       RestartValue => _restartValue;

			private bool _hasStarted;
			private bool _isFinished;
			private Vector3 _restartValue;
			private float _currentTime;

			private Vector3 _start;
			private Vector3 _target;

			public void TryStart(Vector3 initialValue)
			{
				_hasStarted = Behavior != EPlayBehavior.None && Value != Vector3.zero && Duration > 0;

				if (_hasStarted == false)
					return;

				_restartValue = _start = initialValue;
				_target = ValueIsAbsolute == true ? _target : _start + Value;
				_currentTime = -Delay;
				_isFinished = false;
			}

			public void Stop()
			{
				_hasStarted = false;
				_isFinished = false;
			}

			public Vector3 Update()
			{
				_currentTime += Time.deltaTime;

				if (_currentTime >= Duration)
				{
					if (Behavior == EPlayBehavior.Once)
					{
						_isFinished = true;
						return _target;
					}
					else if (Behavior == EPlayBehavior.Restart)
					{
						_currentTime = _currentTime - Duration - Delay;
					}
					else if (Behavior == EPlayBehavior.PingPong)
					{
						var previousStart = _start;
						_start = _target;
						_target = previousStart;
						_currentTime = _currentTime - Duration - Delay;
					}
					else if (Behavior == EPlayBehavior.Continue)
					{
						var delta = _target - _start;
						_start = _target;
						_target = _target + delta;
						_currentTime = _currentTime - Duration - Delay;
					}
				}

				float progress = DOVirtual.EasedValue(0f, 1f, _currentTime / Duration, Ease);
				return Vector3.Lerp(_start, _target, progress);
			}
		}

		public enum EPlayBehavior
		{
			None,
			PingPong,
			Once,
			Restart,
			Continue,
		}
	}
}
