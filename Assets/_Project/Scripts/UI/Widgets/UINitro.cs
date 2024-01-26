using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MoonKart.UI
{
	public class UINitro : UIBehaviour
	{
		// PRIVATE MEMBERS

		[SerializeField]
		private TextMeshProUGUI _caption;
		[SerializeField]
		private Color _depletedColor = Color.white;
		[SerializeField]
		private UIList _ruler;
		[SerializeField]
		private CanvasGroup _rulerFirstPiece;
		[SerializeField]
		private Image _valueImage;

		private float _max;
		private float _value;

		private Color _filledColor;

		// PUBLIC METHODS

		public void SetValue(float value, bool force = false)
		{
			if (value == _value && force == false)
				return;

			DOTween.Kill(_valueImage);

			_valueImage.DOFillAmount(value / _max, 0.2f);
			_valueImage.DOPlay();
			_caption.color = value > 0 ? _filledColor : _depletedColor;

			_value = value;
		}

		public void SetMax(int max)
		{
			if (max == (int)_max)
				return;

			_max = max;

			_rulerFirstPiece.SetVisibility(true);
			_ruler.Refresh(max);
			_rulerFirstPiece.SetVisibility(false);
		}

		// MONOBEHAVIOR

		private void Awake()
		{
			_filledColor = _caption.color;
		}

		private void OnDestroy()
		{
			DOTween.Kill(_valueImage);
		}
	}
}
