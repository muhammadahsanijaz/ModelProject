using UnityEngine;

namespace MoonKart
{
	public sealed class FrameLimiter : CoreBehaviour
	{
#if UNITY_EDITOR
		// PRIVATE MEMBERS

		[SerializeField] Limiter _targetFrameRate;
		[SerializeField] Limiter _processorLoad;

		private int _originalTargetFrameRate;
		private int _originalVSyncCount;

		// MonoBehaviour INTERFACE

		private void OnEnable()
		{
			_originalTargetFrameRate = Application.targetFrameRate;
			_originalVSyncCount      = QualitySettings.vSyncCount;

			_targetFrameRate.Reset();
			_processorLoad.Reset();
		}

		private void Update()
		{
			if (_targetFrameRate.Enabled == true)
			{
				_targetFrameRate.Update(Time.deltaTime);
				SetTargetFrameRate(_targetFrameRate.CurrentValue);
			}
			else
			{
				SetTargetFrameRate(_originalTargetFrameRate);
			}

			if (_processorLoad.Enabled == true)
			{
				_processorLoad.Update(Time.deltaTime);
				ExecuteLoad(_processorLoad.CurrentValue);
			}
		}

		private void OnDisable()
		{
			SetTargetFrameRate(_originalTargetFrameRate);
		}

		// PRIVATE METHODS

		private void SetTargetFrameRate(int frameRate)
		{
			if (Application.targetFrameRate != frameRate)
			{
				Application.targetFrameRate = frameRate;
			}

			QualitySettings.vSyncCount = frameRate <= 0 ? _originalVSyncCount : 0;
		}

		private void ExecuteLoad(int load)
		{
			int value = load * 10000;

			for (int i = 0; i < value; i++)
			{
				float r = Mathf.Pow(10, 15) / Mathf.Tan(45f / 1500f);
			}
		}

		// HELPERS

		[System.Serializable]
		public sealed class Limiter
		{
			public bool   Enabled;
			public int    MinValue;
			public int    MaxValue;
			public float  MinChangeTime;
			public float  MaxChangeTime;

			public int    CurrentValue { get; private set; }

			private int   _targetValue;
			private int   _previousValue;
			private float _totalChangeTime;
			private float _changeTime;

			public void Update(float deltaTime)
			{
				if (_changeTime <= 0f)
				{
					if (MinChangeTime >= 0f)
					{
						_changeTime = MinChangeTime < MaxChangeTime ? Random.Range(MinChangeTime, MaxChangeTime) : MinChangeTime;
						_totalChangeTime = _changeTime;
					}
					else
					{
						_changeTime = 0f;
						_totalChangeTime = 0f;
					}

					_previousValue = _targetValue;
					_targetValue = MinValue < MaxValue ? Random.Range(MinValue, MaxValue + 1) : MinValue;
				}
				else
				{
					_changeTime -= deltaTime;
				}

				if (_totalChangeTime > 0f)
				{
					float progress = 1f - _changeTime / _totalChangeTime;
					CurrentValue = (int)Mathf.Lerp(_previousValue, _targetValue, progress);
				}
				else
				{
					CurrentValue = _targetValue;
				}
			}

			public void Reset()
			{
				CurrentValue     = MinValue;
				_targetValue     = CurrentValue;
				_previousValue   = CurrentValue;

				_changeTime      = 0f;
				_totalChangeTime = 0f;
			}
		}
#endif
	}
}

