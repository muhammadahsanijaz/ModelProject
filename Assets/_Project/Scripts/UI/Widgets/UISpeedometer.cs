using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace MoonKart.UI
{
	public class UISpeedometer : UIBehaviour
	{
		// PRIVATE MEMBERS

		[SerializeField]
		private TextMeshProUGUI _valueText;
		[SerializeField]
		private Image _valueImage;
		[SerializeField]
		private string _speedFormat = "f0";
		[SerializeField]
		private bool _useMetersPerSecond;
		[SerializeField]
		private CanvasGroup _reverseGroup;
		[SerializeField]
		private TextMeshProUGUI _speedUnits;

		private float _value = -1;
		private float _max = -1;

		private int _lastTextValue = - 1;

		// PUBLIC METHODS

		public void SetValue(float value, bool force = false)
		{
			if (value == _value && force == false)
				return;

			DOTween.Kill(_valueImage);

			_valueImage.DOFillAmount(value / _max, 0.2f);
			_valueImage.DOPlay();

			int textValue = _useMetersPerSecond == true ? (int)value : (int)(value * 3.6f);
			if (textValue != _lastTextValue)
			{
				_lastTextValue = textValue;
				_valueText.text = Mathf.Abs(_lastTextValue).ToString(_speedFormat);
			}

			if (_reverseGroup != null)
			{
				_reverseGroup.alpha = value < -1f ? 1f : 0f;
			}

			_value = value;
		}

		public void SetMax(int max)
		{
			_max = max;
		}

		// MONOBEHAVIOR

		protected void Awake()
		{
			if (_speedUnits != null)
			{
				_speedUnits.text = _useMetersPerSecond == true ? "m/s" : "km/h";
			}
		}

		protected void OnDestroy()
		{
			DOTween.Kill(_valueImage);
		}
	}
}
