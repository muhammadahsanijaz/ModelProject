using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace MoonKart.UI
{
    public class UIShopPanelView : UICloseView
    {
        [TabGroup("General")] [SerializeField] private GameObject bg;
        [TabGroup("General")] [SerializeField] private CinemachineVirtualCamera camera;


        [TabGroup("UI")] [SerializeField] private TextMeshProUGUI TitleText;
        [TabGroup("UI")] public Transform buttonsContainerTransform;
        [TabGroup("UI")] [SerializeField] private List<GameObject> shopUIButtons;
        [TabGroup("UI")] [SerializeField] private GameObject shopUIButtonPrefab;
        protected override void OnInitialize()
        {
            base.OnInitialize();
            ShowPackButtons();
        }

        private void OnStackButton()
        {
            //TODO Stack Calculation
        }

        protected override void OnDeinitialize()
        {
            base.OnDeinitialize();
        }

        protected override void OnOpen(params object[] cardInfo)
        {
            base.OnOpen(cardInfo);

            if (bg)
                bg.SetActive(true);
            if (camera)
                camera.enabled = true;


            TitleText.text = "Shop";
            
        }

        protected override void OnClose()
        {
            if (bg)
                bg.SetActive(false);
            if (camera)
                camera.enabled = false;
        }

        private void ShowPackButtons()
        {
            //foreach (var pack in Global.Settings.PacksDataModel.packs)
            //{
            //    var packButtonInstance = Instantiate(shopUIButtonPrefab, buttonsContainerTransform).GetComponent<ShopUIButton>();
            //    packButtonInstance.Initialize(pack);
            //    packButtonInstance.onClick.AddListener(OnCloseCallBack);
            //}
        }

        private void OnCloseCallBack()
        {
            
        }
    }
}