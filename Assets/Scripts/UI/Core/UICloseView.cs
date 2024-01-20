using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonKart.UI
{
    /// <summary>
    /// Use For back FFunctionalities 
    /// </summary>
    public class UICloseView : UIView
    {
        // PUBLIC MEMBERS

        //  Set The View That You want to open on backButton Pressed
        public UIView BackView { get; set; }

        //Back Button Getter
        public UIButton CloseButton => _closeButton;

        // PRIVATE MEMBERS

        //Back Button
        [TabGroup("General")] [SerializeField] private UIButton _closeButton;


        // PUBLIC METHODS

        public void CloseWithBack()
        {
            OnCloseButton();
        }

        // UIVIEW INTERFACE

        protected override void OnInitialize()
        {
            base.OnInitialize();

            if (_closeButton != null)
            {
                _closeButton.onClick.AddListener(OnCloseButton);
            }
        }

        protected override void OnDeinitialize()
        {
            if (_closeButton != null)
            {
                _closeButton.onClick.RemoveListener(OnCloseButton);
            }

            base.OnDeinitialize();
        }

    

        protected override bool OnBackAction()
        {
            if (IsInteractable == false)
                return false;

            OnCloseButton();
            return true;
        }

        // PROTECTED METHODS

        protected virtual void OnCloseButton()
        {
            Close();

            if (BackView != null)
            {
                Open(BackView);
                BackView = null;
            }
        }
    }
}