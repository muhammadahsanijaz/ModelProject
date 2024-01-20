
using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace MoonKart.UI
{
    public class UIStoreView : UICloseView
    {
        [SerializeField] private CinemachineVirtualCamera _camera;

        protected override void OnInitialize()
        {
            base.OnInitialize();
        }


        protected override void OnDeinitialize()
        {
            base.OnDeinitialize();
        }


        protected override void OnOpen(params object[] cardInfo)
        {
            base.OnOpen(cardInfo);
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            _camera.enabled = true;
        }


        protected override void OnClose()
        {
            _camera.enabled = false;

            base.OnClose();
        }
    }
}