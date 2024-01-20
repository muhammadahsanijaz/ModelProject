using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace MoonKart.UI
{
    public class UIGameplayView : UIView
    {

        [SerializeField] private UISpeedometer _speedometer;
        [SerializeField] private TextMeshProUGUI realSpeed;
        [SerializeField] private Slider healthSlider;
        // UIView INTERFACE

        
        protected override void OnInitialize()
        {
            base.OnInitialize();
        }

        protected override void OnDeinitialize()
        {
            base.OnDeinitialize();
        }

        // PRIVATE METHODS

        private void OnLocalPlayerChanged()
        {
            Open();
        }
        
        protected override void OnGameSet()
        {
            base.OnGameSet();
        }

        protected override void OnGameCleared()
        {
            base.OnGameCleared();
        }

        protected override void OnTick()
        {
            base.OnTick();

            //int totalLaps = frame.RuntimeConfig.Laps;
            //int currentLap = Mathf.Clamp(player.FinishedLaps + 1, 1, totalLaps);

            //_speedometer.SetValue(vehicle.currentSpeed.AsFloat);
            //realSpeed.text = vehicle.currentSpeed.AsInt + "";

            //_raceProgress.SetLaps(currentLap, totalLaps);
            //_raceProgress.SetPosition(player.Statistics.Position, frame.RuntimeConfig.PlayerCount);
            //_raceProgress.SetTime(GetRaceTime(ref gameplay, ref player));

            //healthSlider.value = vehicle.currentHealth.AsInt;

            //_raceProgress.SetCoins(GetTrunkCoins(ref coinsManager, ref player),coinsManager.TrunkSpace.AsInt);

        }

        //PRIVATE METHODS

        private void OnLapFinished()
        {

        }

        private void OnPlayerFinished()
        {
            CanvasGroup.DOFade(0f, 3f).SetAutoKill().Play();
        }

        private float GetRaceTime()
        {

            return 0;// gameplay.StateTime.AsFloat;
        }
        
        private int GetTrunkCoins()
        {
            return 0; //coinsManager.TrunkFill.AsInt;
        }
    }
}