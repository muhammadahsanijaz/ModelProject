using System;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace MoonKart.UI
{
	public class UIPlayer : UIBehaviour
	{
		// PRIVATE MEMBERS

		[SerializeField]
		private TextMeshProUGUI _name;
		[SerializeField]
		private TextMeshProUGUI _description;
		 
		public Slider speedSlider;
		public Slider handlingSlider;
		public Slider accelerationSlider;
		public Slider HealthSlider;
    
		public TextMeshProUGUI speedCurrentValue;
		public TextMeshProUGUI handlingCurrentValue;
		public TextMeshProUGUI accelerationCurrentValue;
		public TextMeshProUGUI healthCurrentValue;

		private PlayerSetup playerSetup; 

		private void Start()
		 {
			speedSlider.onValueChanged.AddListener((value) =>
			{
				//carSetup.MaxSpeed =  FP.FromFloat_UNSAFE(speedSlider.value) * Global.Settings.Car.MaxSpeed / FP._100;
				speedCurrentValue.text = speedSlider.value + "";
			});
        
			handlingSlider.onValueChanged.AddListener((value) =>
			{
				//carSetup.Handling =  FP.FromFloat_UNSAFE(handlingSlider.value) * Global.Settings.Car.MaxHandling / FP._100;
				handlingCurrentValue.text = handlingSlider.value + "";
			});
        
			accelerationSlider.onValueChanged.AddListener((value) =>
			{
				//carSetup.Acceleration =  FP.FromFloat_UNSAFE(accelerationSlider.value) * Global.Settings.Car.MaxAcceleration / FP._100;
				accelerationCurrentValue.text = accelerationSlider.value + "";
			});
        
			HealthSlider.onValueChanged.AddListener((value) =>
			{
				//carSetup.Health =  FP.FromFloat_UNSAFE(HealthSlider.value) * Global.Settings.Car.MaxHealth / FP._100;
				healthCurrentValue.text = HealthSlider.value + "";
			});
		}

		// PUBLIC METHODS

		public void SetData(PlayerSetup playerSetup)
		{
			this.playerSetup = playerSetup;
			if (_name != null)
			{
                _name.text = this.playerSetup.DisplayName;
			}
			if (_description != null)
			{
                _description.text = this.playerSetup.Description;
			}

			UpdateCarState();
			// if (_difficulty != null)
			// {
			// 	_difficulty.text = string.Format(_difficultyFormat, carSetup.Difficulty.ToString());
			// }
		}
		
		void UpdateCarState()
		{
			// speedSlider.value =   carSetup.MaxSpeed .AsFloat;
			// handlingSlider.value =   carSetup.Handling .AsFloat;
			// accelerationSlider.value =   carSetup.Acceleration .AsFloat;
			// HealthSlider.value =  carSetup.Health .AsFloat;
			//Debug.LogError(carSetup.VehicleData.VehicleStats.Speed + "-" 
			//	+ carSetup.VehicleData.VehicleStats.Handling + "-"
			//	+ carSetup.VehicleData.VehicleStats.Acceleration + "-"
			//	+ carSetup.VehicleData.VehicleStats.Health);
			//speedSlider.DOValue(  carSetup.VehicleData.VehicleStats.Speed,.1f).SetEase(Ease.Linear);
			//handlingSlider.DOValue(   carSetup.VehicleData.VehicleStats.Handling, .1f).SetEase(Ease.Linear);
			//accelerationSlider.DOValue(  carSetup.VehicleData.VehicleStats.Acceleration, .1f).SetEase(Ease.Linear);
			//HealthSlider.DOValue(   carSetup.VehicleData.VehicleStats.Health, .1f).SetEase(Ease.Linear);
		}
	}
}
