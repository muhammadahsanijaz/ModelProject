using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace MoonKart.UI
{
    public class AnimateHover : UIBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] AudioSetup hoverAudioSetup;
        private Vector3 orignalScale = Vector3.zero;
        public Animator anime;
        private bool clicked = true;

        public void Initialize()
        {
            orignalScale = RectTransform.localScale;

            if (anime == null)
            {
                if (orignalScale != Vector3.zero)
                {
                    RectTransform.DOScale(orignalScale, 0);
                }
                else
                {
                    RectTransform.DOScale(Vector3.one, 0);
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           
            if (anime == null)
            {
                if (orignalScale != Vector3.zero)
                {
                    RectTransform.DOScale((orignalScale + (Vector3.one * 0.1f)), 0.2f);
                }
                else
                {
                    RectTransform.DOScale(Vector3.one * 1.1f, 0.2f);
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (anime == null)
            {
                if (orignalScale != Vector3.zero)
                {
                    RectTransform.DOScale(orignalScale, 0.2f);
                }
                else
                {
                    RectTransform.DOScale(Vector3.one, 0.2f);
                }
            }
        }


        public void OnDestroy()
        {

        }
    }
}