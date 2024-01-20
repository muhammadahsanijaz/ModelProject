using MoonKart.UI;
using UnityEngine;
using UnityEngine.UI;

namespace MoonKart
{
    public static class UIExtensions
    {
        public static void SetActive(this RectTransform @this, bool value)
        {
            if (@this == null)
                return;

            if (@this.gameObject.activeSelf == value)
                return;

            @this.gameObject.SetActive(value);
        }

        public static void SetActive(this SimpleAnimation @this, bool value)
        {
            if (@this == null)
                return;

            if (@this.gameObject.activeSelf == value)
                return;

            @this.gameObject.SetActive(value);
        }

        public static void SetActive(this UnityEngine.EventSystems.UIBehaviour @this, bool value)
        {
            if (@this == null)
                return;

            if (@this.gameObject.activeSelf == value)
                return;

            @this.gameObject.SetActive(value);
        }

        public static void SetActive(this CanvasGroup @this, bool value)
        {
            if (@this == null)
                return;

            if (@this.gameObject.activeSelf == value)
                return;

            @this.gameObject.SetActive(value);
        }

        public static void SetActive(this UIWidget @this, bool value)
        {
            if (@this == null)
                return;

            if (@this.gameObject.activeSelf == value)
                return;

            @this.gameObject.SetActive(value);
        }

        public static void SetActive(this UIBehaviour @this, bool value)
        {
            if (@this == null)
                return;

            if (@this.gameObject.activeSelf == value)
                return;

            @this.gameObject.SetActive(value);
        }

        public static void SetVisibility(this CanvasGroup @this, bool value)
        {
            if (@this == null)
                return;

            @this.alpha = value == true ? 1f : 0f;
            @this.interactable = value;
            @this.blocksRaycasts = value;
        }

        public static void SetTextSafe(this TMPro.TextMeshProUGUI @this, string text)
        {
            if (@this == null)
                return;
            if (string.IsNullOrEmpty(text))
            {
                @this.text = "";
                return;
            }

            @this.text = text;
        }

        public static void SetSpriteSafe(this Image @this, Sprite sprite)
        {
            if (@this == null)
                return;

            @this.sprite = sprite;
        }

        public static string GetTextSafe(this TMPro.TextMeshProUGUI @this)
        {
            if (@this == null)
                return null;

            return @this.text;
        }
    }

    public static class UISliderExtensions
    {
        public static void SetOptionsValue(this UISlider slider, OptionsValue value)
        {

            if (slider.minValue != value.FloatValue.MinValue || slider.maxValue != value.FloatValue.MaxValue)
            {
                // Setting min and max value will unfortunately always trigger onValueChanged
                // so we need to removed this event data and reassign it again agterwards

                Slider.SliderEvent onValueChanged = slider.onValueChanged;
                slider.onValueChanged = new Slider.SliderEvent();

                slider.minValue = value.FloatValue.MinValue;
                slider.maxValue = value.FloatValue.MaxValue;

                slider.onValueChanged = onValueChanged;
            }

            slider.SetValue(value.FloatValue.Value);
        }
    }
}