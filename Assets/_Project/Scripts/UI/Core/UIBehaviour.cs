using UnityEngine;
using UnityEngine.UI;

namespace MoonKart.UI
{
	/// <summary>
	/// UIBehaviour  Give Us
	/// Canvas Group,
	/// RectTransform
	/// </summary>
	public class UIBehaviour : CoreBehaviour
	{
		// PUBLIC MEMBERS

		public Image Image
		{
			get
			{
				if (_ImageChecked == false)
				{
					_cachedImage = GetComponent<Image>();
					_ImageChecked = true;
				}

				return _cachedImage;
			}
		}

		public CanvasGroup CanvasGroup
		{
			get
			{
				if (_canvasGroupChecked == false)
				{
					_cachedCanvasGroup = GetComponent<CanvasGroup>();
					_canvasGroupChecked = true;
				}

				return _cachedCanvasGroup;
			}
		}
		

		public RectTransform RectTransform
		{
			get
			{
				if (_rectTransformChecked == false)
				{
					_cachedRectTransform = transform as RectTransform;
					_rectTransformChecked = true;
				}

				return _cachedRectTransform;
			}
		}


		// PRIVATE MEMBERS

		private Image _cachedImage;
		private bool _ImageChecked;

		private CanvasGroup _cachedCanvasGroup;
		private bool _canvasGroupChecked;

		private RectTransform _cachedRectTransform;
		private bool _rectTransformChecked;

	}
}
