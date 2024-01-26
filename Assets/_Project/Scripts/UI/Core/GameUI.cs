using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MoonKart.UI
{
    public class GameUI : GameService, IBackHandler
    {
        // PUBLIC MEMBERS

        public Camera UICamera { get; private set; }
        
        [SerializeField]
        private AudioSetup _clickSound;
        [SerializeField]
        private AudioEffect[] _audioEffects;

        private ScreenOrientation _lastScreenOrientation;

        // GameUI INTERFACE

        protected UIView[] _views;

        protected virtual void OnInitializeInternal()
        {
        }

        protected virtual void OnDeinitializeInternal()
        {
        }

        protected virtual void OnGameSetInternal()
        {
        }

        protected virtual void OnGameClearedInternal()
        {
        }

        protected virtual void OnTickInternal()
        {
        }

        protected virtual bool OnBackAction()
        {
            return false;
        }

        // PUBLIC METHODS


        /// <summary>
        /// Get any View 
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : class
        {
            if (_views == null)
                return null;

            for (int i = 0; i < _views.Length; ++i)
            {
                T view = _views[i] as T;

                if (view != null)
                    return view;
            }

            return null;
        }

        /// <summary>
        /// Open View 
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Open<T>() where T : UIView
        {
            if (_views == null)
                return null;

            for (int i = 0; i < _views.Length; ++i)
            {
                T view = _views[i] as T;
                if (view != null)
                {
                    view.Open();
                    return view;
                }
            }

            return null;
        }

        /// <summary>
        /// Open View With passing some parameter
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Open<T>(params object[] obj) where T : UIView
        {
            if (_views == null)
                return null;

            for (int i = 0; i < _views.Length; ++i)
            {
                T view = _views[i] as T;
                if (view != null)
                {
                    view.Open(obj);
                    return view;
                }
            }

            return null;
        }

        /// <summary>
        /// Open View
        /// </summary>
        /// <param name="view">Any type of View</param>
        /// <typeparam name="T">T is data type</typeparam>
        /// <returns></returns>
        public T Open<T>(T view) where T : UIView
        {
            if (_views == null)
                return null;

            int index = Array.IndexOf(_views, view);

            if (index < 0)
            {
                Debug.LogError($"Cannot find view {view.name}");
                return null;
            }

            view.Open();

            return view;
        }

        /// <summary>
        /// Assign a view witch will open when back button pressed
        /// </summary>
        /// <param name="backView"></param>
        /// <returns></returns>
        public T OpenWithBackView<T>(UIView backView) where T : UICloseView
        {
            T view = Open<T>();

            if (view is UICloseView closeView)
            {
                closeView.BackView = backView;
            }

            return view;
        }

        /// <summary>
        /// Assign a view witch will open when back button pressed
        /// </summary>
        /// <param name="backView"></param>
        /// <param name="obj">Any type Of Data you want you send on View Open</param>
        /// <returns></returns>
        public T OpenWithBackView<T>(UIView backView, params object[] obj) where T : UICloseView
        {
            T view = Open<T>(obj);

            if (view is UICloseView closeView)
            {
                closeView.BackView = backView;
            }

            return view;
        }

        public T Close<T>() where T : UIView
        {
            if (_views == null)
                return null;

            for (int i = 0; i < _views.Length; ++i)
            {
                T view = _views[i] as T;
                if (view != null)
                {
                    view.Close();
                    return view;
                }
            }

            return null;
        }

        /// <summary>
        /// Check If it is On then turn it off and if it is off then turn it on
        /// </summary>
        /// <typeparam name="T"> Any type of view </typeparam>
        /// <returns></returns>
        public T Toggle<T>() where T : UIView
        {
            if (_views == null)
                return null;

            for (int i = 0; i < _views.Length; ++i)
            {
                T view = _views[i] as T;
                if (view != null)
                {
                    if (view.IsOpen == true)
                    {
                        view.Close();
                    }
                    else
                    {
                        view.Open();
                    }

                    return view;
                }
            }

            return null;
        }

        /// <summary>
        /// Check If View is Open or not
        /// </summary>
        /// <typeparam name="T">Any type of view</typeparam>
        /// <returns></returns>
        public bool IsOpen<T>() where T : UIView
        {
            if (_views == null)
                return false;

            for (int i = 0; i < _views.Length; ++i)
            {
                T view = _views[i] as T;
                if (view != null)
                {
                    return view.IsOpen;
                }
            }

            return false;
        }

        /// <summary>
        /// Close all open views
        /// </summary>
        public void CloseAll()
        {
            if (_views == null)
                return;

            for (int i = 0; i < _views.Length; ++i)
            {
                UIView view = _views[i];
                if (view != null)
                {
                    view.Close();
                }
            }
        }

        /// <summary>
        /// Get all Element of Specific Type
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public void GetAll<T>(List<T> list)
        {
            if (_views == null)
                return;

            for (int i = 0; i < _views.Length; ++i)
            {
                if (_views[i] is T element)
                {
                    list.Add(element);
                }
            }
        }

        public bool PlaySound(AudioSetup effectSetup)
        {
            for (int i = 0; i < _audioEffects.Length; i++)
            {
                AudioEffect effectSound = _audioEffects[i];

                if (effectSound.IsPlaying == false)
                {
                    effectSound.Play(effectSetup);
                    return true;
                }
            }

            return false;
        }

        public bool PlayClickSound()
        {
            return PlaySound(_clickSound);
        }

        // IBackHandler INTERFACE

        int IBackHandler.Priority => -1;
        bool IBackHandler.IsActive => true;

        bool IBackHandler.OnBackAction()
        {
            return OnBackAction();
        }

        // GameService INTERFACE

        protected override sealed void OnInitialize()
        {
            UICamera = GetComponent<Canvas>().worldCamera;

            _views = GetComponentsInChildren<UIView>(true);
            for (int i = 0; i < _views.Length; ++i)
            {
                UIView view = _views[i];

                view.Initialize(this, null);
                view.SetPriority(i);

                view.gameObject.SetActive(false);
            }

            OnInitializeInternal();

            //UpdateScreenOrientation();
        }

        protected override sealed void OnDeinitialize()
        {
            OnDeinitializeInternal();

            if (_views != null)
            {
                for (int i = 0; i < _views.Length; ++i)
                {
                    _views[i].Deinitialize();
                }

                _views = null;
            }
        }

        protected override sealed void OnGameSet()
        {
            if (_views != null)
            {
                for (int i = 0; i < _views.Length; ++i)
                {
                    _views[i].GameSet();
                }
            }

            OnGameSetInternal();
        }

        protected override sealed void OnGameCleared()
        {
            OnGameClearedInternal();

            if (_views != null)
            {
                for (int i = 0; i < _views.Length; ++i)
                {
                    var view = _views[i];

                    if (view != null)
                    {
                        view.GameCleared();
                    }
                }
            }
        }

        protected override sealed void OnTick()
        {
            if (_views != null)
            {
                for (int i = 0; i < _views.Length; ++i)
                {
                    UIView view = _views[i];
                    if (view.IsOpen == true)
                    {
                        view.Tick();
                    }
                }
            }

            OnTickInternal();

            //UpdateScreenOrientation();
        }

        // PRIVATE METHODS

        private void UpdateScreenOrientation()
        {
            ScreenOrientation orientation = Screen.orientation;
            if (orientation == _lastScreenOrientation)
                return;

            for (int idx = 0, count = _views.Length; idx < count; idx++)
            {
                _views[idx].UpdateSafeArea();
            }

            _lastScreenOrientation = orientation;
        }
    }
}