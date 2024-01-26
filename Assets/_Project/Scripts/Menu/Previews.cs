using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace MoonKart
{
    public class Previews : CoreBehaviour
    {
        // PRIVATE MEMBERS
        
        [SerializeField] private PlayerPreview _playerPreview;
        [SerializeField] private float _rotationSpeed = 10;
        [SerializeField] private float _rotationDelay = 2f;

        [SerializeField] private float garageRotationAngle = 45.0f;

        private Coroutine _rotationRoutine;
        private Quaternion _initialRotation;

        // PUBLIC MEMBERS
        private Tween mainMenuRotation;
        private Tween garageRoattion;


        public void ShowPlayer(string playerID, bool rotate, bool force = false)
        {
            if (playerID == _playerPreview.PlayerID && force == false)
                return;

            _playerPreview.ShowPlayer(playerID);

            if (rotate == true)
            {
                garageRoattion.Pause();
                if (mainMenuRotation == null)
                    mainMenuRotation = _playerPreview.transform.DORotate(new Vector3(0, 45, 0), 2f)
                        .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
                else
                    mainMenuRotation.Play();
            }
        }

        public void ShowPlayer()
        {
            mainMenuRotation.Pause();
            _playerPreview.transform.DORotate(new Vector3(0, 45, 0), .5f).SetEase(Ease.OutBack);
        }


        public void HidePlayer()
        {
            _playerPreview.transform.rotation = Quaternion.identity;
            _playerPreview.HidePlayer();
        }



        // MONOBEHAVIOR

        private void Awake()
        {
            _initialRotation = _playerPreview.transform.localRotation;
        }

       

        // // PRIVATE MEMBERS
        // private IEnumerator Rotate_Coroutine()
        // {
        //     yield return WaitFor.Seconds(_rotationDelay);
        //     // rotation.Kill();
        //     // rotation = _playerPreview.transform.DORotate(new Vector3(0, 45, 0), .5f)
        //     //     .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        //     // float currentSpeed = 0f;
        //     //
        //     // while (true)
        //     // {
        //     //     currentSpeed = Mathf.Lerp(currentSpeed, _rotationSpeed, Time.deltaTime);
        //     //
        //     //     _playerPreview.transform.Rotate(Vector3.up, currentSpeed * Time.deltaTime);
        //     //     yield return null;
        //     // }
        // }

        // private float currentAngle;
        // private float currentSpeed = 0f;

        // private IEnumerator Rotate_CoroutineGarage()
        // {
        //     // rotation.Kill();
        //     // _playerPreview.transform.DORotate(new Vector3(0, 45, 0), .5f).SetEase(Ease.OutBack);
        //     // var angel = _playerPreview.transform.rotation.eulerAngles.y ;
        //     // if (angel > garageRotationAngle + 5 || angel < garageRotationAngle - 5 )
        //     // {
        //     //     currentSpeed = Mathf.Lerp(currentSpeed, _rotationSpeed, Time.deltaTime);
        //     //
        //     //     currentAngle = currentSpeed * Time.deltaTime * 30;
        //     //     _playerPreview.transform.Rotate(Vector3.up, currentAngle);
        //     //     Debug.LogError(_playerPreview.transform.rotation.eulerAngles.y);
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