using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MoonKart.UI
{
    public class UISimpleNotificationView : UIView
    {
        [SerializeField] private SimpleAnimation _notificationGroup;

        [SerializeField] private TextMeshProUGUI _notificationText;
        [SerializeField] private UIButton _cancelButton;

        private Func<bool> _cancelButtonCallback;

        // PUBLIC METHODS

        public void ShowNotification(string text, Func<bool> cancelButtonCallback)
        {
            _cancelButtonCallback = cancelButtonCallback;


            _notificationText.text = text;
            _notificationGroup.SetActive(true);
        }


        public void CancelNotification()
        {
            _notificationGroup.SetActive(false);
            _cancelButtonCallback = null;
        }

        // UIView INTEFACE

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _notificationGroup.SetActive(false);
            _cancelButton.onClick.AddListener(OnCancelButton);
        }

        protected override void OnDeinitialize()
        {
            _cancelButton.onClick.RemoveListener(OnCancelButton);

            base.OnDeinitialize();
        }

        // PRIVATE METHODS

        private void OnCancelButton()
        {
            if (_cancelButtonCallback == null || _cancelButtonCallback.Invoke() == true)
            {
                CancelNotification();
            }
        }
    }
}