using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MoonKart.UI
{
    public class UIGameEndCountDown : UIView
    {
  
        // PRIVATE MEMBERS
       
        [SerializeField]
        private TextMeshProUGUI _text;
        // [SerializeField]
        // private int _startFrom = 10;
       
        private int _lastTime = int.MaxValue;
       
        // UIView INTERFACE
       
        protected override void OnOpen()
        {
            base.OnOpen();
       
            _text.text = string.Empty;
        }
       
        protected override void OnTick()
        {
            base.OnTick();



            //int remainingTime = frame.RuntimeConfig.GameEndWaitTime.AsInt - gameplay.StateTime.AsInt;
            int remainingTime = 0;// gameplay.GameEndCounter.AsInt;
       
            // Debug.LogError($"remainingTime {remainingTime} : _lastTime {_lastTime}  ");
            if (remainingTime <= _lastTime && remainingTime >= 0)
            {
                ShowTime(remainingTime);
                //TODo Play Sound
            }
        }
       
        // PRIVATE METHODS
       
        private void ShowTime(int time)
        {
            _text.text = time.ToString();
            _lastTime = time;
        }
    }
}