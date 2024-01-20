using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class RotateTheWheel : MonoBehaviour
    {
        private float rotationTime = 2;

        private float timer = 0;
        private bool starRotating = false;
        private void Start()
        {
            //rect.DORotate(new Vector3(0,0,180),2f).SetEase(Ease.Linear).SetLoops(2);
           // ac = gameObject.GetComponent<Animator>();
           
            // starRotating = true;
        }


        private void Update()
        {
            if (starRotating)
            {            
                timer += Time.deltaTime;  
                // transform.Rotate(0, 0, rotateSpeed);           
              //  rotateSpeed *= 0.99f;
        
                if (timer >= rotationTime)
                {
                    timer = 0;
                    starRotating = false;
                }
        
            }
        }

    }
}