using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace MoonKart
{
    public class Garage : CoreBehaviour
    {
        // PRIVATE MEMBERS

        [SerializeField] private DriverPreview _driverPreview;
        [SerializeField] private CarPreview _carPreview;
        [SerializeField] private CarPreview[] _matchCarPreviews;
        [SerializeField] private float _rotationSpeed = 10;
        [SerializeField] private float _rotationDelay = 2f;

        [SerializeField] private float garageRotationAngle = 45.0f;

        private Coroutine _rotationRoutine;
        private Quaternion _initialRotation;

        // PUBLIC MEMBERS
        private Tween mainMenuRotation;
        private Tween garageRoattion;


        public void ShowDriver(string carID, bool rotate, bool force = false)
        {
            if (carID == _driverPreview.DriverID && force == false)
                return;


            _driverPreview.ShowDriver(carID);

            if (rotate == true)
            {
                garageRoattion.Pause();
                if (mainMenuRotation == null)
                    mainMenuRotation = _driverPreview.transform.DORotate(new Vector3(0, 45, 0), 2f)
                        .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
                else
                    mainMenuRotation.Play();
                //_rotationRoutine = StartCoroutine(Rotate_Coroutine());
            }
            HideCar();
        }

        public void ShowCar(string carID, bool rotate, bool force = false)
        {
            if (carID == _carPreview.CarID && force == false)
                return;

            //_carPreview.transform.localRotation = _initialRotation;
            
            _carPreview.ShowCar(carID);

            if (rotate == true)
            {
                garageRoattion.Pause();
                if (mainMenuRotation == null)
                    mainMenuRotation = _carPreview.transform.DORotate(new Vector3(0, 45, 0), 2f)
                        .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
                else
                    mainMenuRotation.Play();
                //_rotationRoutine = StartCoroutine(Rotate_Coroutine());
            }
            HideDriver();
        }

        public void ShowCar()
        {
            mainMenuRotation.Pause();
            // if (garageRoattion == null)
            //     garageRoattion = _carPreview.transform.DORotate(new Vector3(0, 45, 0), .5f).SetEase(Ease.OutBack);
            // else
                _carPreview.transform.DORotate(new Vector3(0, 45, 0), .5f).SetEase(Ease.OutBack);
            // _rotationRoutine = StartCoroutine(Rotate_CoroutineGarage());
        }

        public void HideDriver()
        {

            _driverPreview.HideDriver();
        }

        public void HideCar()
        {
            _carPreview.transform.rotation = Quaternion.identity;
            _carPreview.HideCar();
        }

        public void ShowMatchCar(int slot, string carID)
        {
            var carPreview = _matchCarPreviews[slot];
            carPreview.ShowCar(carID);
            _carPreview.HideCar();
        }

        public void HideMatchCar(int slot)
        {
            _matchCarPreviews[slot].HideCar();
        }

        public void HideMatchCarsFromSlot(int fromSlot)
        {
            for (int i = fromSlot; i < _matchCarPreviews.Length; i++)
            {
                _matchCarPreviews[i].HideCar();
            }
        }

        public void HideAllMatchCars()
        {
            for (int i = 0; i < _matchCarPreviews.Length; i++)
            {
                _matchCarPreviews[i].HideCar();
            }
        }

        // MONOBEHAVIOR

        private void Awake()
        {
            _initialRotation = _carPreview.transform.localRotation;
        }

       

        // // PRIVATE MEMBERS
        // private IEnumerator Rotate_Coroutine()
        // {
        //     yield return WaitFor.Seconds(_rotationDelay);
        //     // rotation.Kill();
        //     // rotation = _carPreview.transform.DORotate(new Vector3(0, 45, 0), .5f)
        //     //     .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        //     // float currentSpeed = 0f;
        //     //
        //     // while (true)
        //     // {
        //     //     currentSpeed = Mathf.Lerp(currentSpeed, _rotationSpeed, Time.deltaTime);
        //     //
        //     //     _carPreview.transform.Rotate(Vector3.up, currentSpeed * Time.deltaTime);
        //     //     yield return null;
        //     // }
        // }

        // private float currentAngle;
        // private float currentSpeed = 0f;

        // private IEnumerator Rotate_CoroutineGarage()
        // {
        //     // rotation.Kill();
        //     // _carPreview.transform.DORotate(new Vector3(0, 45, 0), .5f).SetEase(Ease.OutBack);
        //     // var angel = _carPreview.transform.rotation.eulerAngles.y ;
        //     // if (angel > garageRotationAngle + 5 || angel < garageRotationAngle - 5 )
        //     // {
        //     //     currentSpeed = Mathf.Lerp(currentSpeed, _rotationSpeed, Time.deltaTime);
        //     //
        //     //     currentAngle = currentSpeed * Time.deltaTime * 30;
        //     //     _carPreview.transform.Rotate(Vector3.up, currentAngle);
        //     //     Debug.LogError(_carPreview.transform.rotation.eulerAngles.y);
        //     yield return null;
        //     //     StartCoroutine(Rotate_CoroutineGarage());
        //     // }
        //     // else
        //     // {
        //     //     StopRotation();
        //     // }
        // }

    }
}