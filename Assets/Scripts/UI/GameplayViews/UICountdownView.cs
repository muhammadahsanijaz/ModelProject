using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MoonKart.UI
{
    public class UICountdownView : UIView
    {
        // PRIVATE MEMBERS

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private int _startFrom = 3;

        private int _lastTime = int.MaxValue;

        // UIView INTERFACE

        protected override void OnOpen()
        {
            base.OnOpen();

            _text.text = string.Empty;
        }

        protected override void OnTick()
        {
            //int remainingTime = StartTime - StateTime;

            //if (remainingTime < _lastTime && remainingTime > 0 && remainingTime <= _startFrom)
            //{
            //    ShowTime(remainingTime);
            //    //TODo Play Sound
            //}
        }

        // PRIVATE METHODS

        private void ShowTime(int time)
        {
            _text.text = time.ToString();

            // _text.transform.localScale = Vector3.one;
            // _text.transform.DOScale(2f, 0.8f);
            //
            // _text.alpha = 1f;
            //
            // var sequence = DOTween.Sequence();
            // sequence.AppendInterval(0.4f);
            // sequence.Append(_text.DOFade(0f, 0.4f));

            _lastTime = time;
        }
    }
}