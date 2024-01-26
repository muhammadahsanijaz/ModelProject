using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace MoonKart.UI
{
	public class UISlider : Slider
	{
		// PRIVATE MEMBERS

		[SerializeField]
		private TextMeshProUGUI  _valueText;
		[SerializeField]
		private TextMeshProUGUI  _maxValueText;
		[SerializeField]
		private TextMeshProUGUI _slashText;
		[SerializeField]
		private string           _valueFormat = "f1";
		[SerializeField]
		private bool             _playValueChangedSound = true;
		internal bool _overrideSlash = false;


		private UIWidget         _parent;

		// PUBLIC METHODS

		public void SetValue(float value)
		{
			SetValueWithoutNotify(value);
			UpdateValueText();
		}

		// MONOBEHAVIOR

		protected override void Awake()
		{
			base.Awake();

			onValueChanged.AddListener(OnValueChanged);
		}

		protected override void OnDestroy()
		{
			onValueChanged.RemoveListener(OnValueChanged);

			base.OnDestroy();
		}

		// Slider INTERFACE

		public override void OnPointerDown(PointerEventData eventData)
		{
			if (IsActive() && IsInteractable() && eventData.button == PointerEventData.InputButton.Left)
			{
				PlayValueChangedSound();
			}

			base.OnPointerDown(eventData);
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				PlayValueChangedSound();
			}

			base.OnPointerUp(eventData);
		}

		// PRIVATE METHODS

		private void OnValueChanged(float value)
		{
			UpdateValueText();
		}

		private void UpdateValueText()
		{
			if (_overrideSlash)
			{
				if (_valueText != null) _valueText.text = "";
				if (_slashText != null) _slashText.text = "";
				if(_maxValueText!= null) _maxValueText.text = "+" + value.ToString(_valueFormat);
			}
			else if(value == 0)
            {
				if (_valueText != null) _valueText.text = "";
				if (_slashText != null) _slashText.text = "";
				if (_maxValueText != null) _maxValueText.text = "-";
			}
			else
			{
				if (_valueText != null) _valueText.text = value.ToString(_valueFormat);
				if (_slashText != null) _slashText.text = "/".ToString();
				if (_maxValueText != null) _maxValueText.text = maxValue.ToString(_valueFormat);
			}
		}

		private void PlayValueChangedSound()
		{
			if (_playValueChangedSound == false)
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
