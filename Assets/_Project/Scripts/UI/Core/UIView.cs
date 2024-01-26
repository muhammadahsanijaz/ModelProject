using System;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace MoonKart.UI
{
	public interface IDelayBlurView
	{
		int DelayFrames { get; }
	}

	[RequireComponent(typeof(CanvasGroup))]
	public abstract class UIView : UIWidget, IBackHandler
	{
		// PUBLIC MEMBERS

		public event Action HasOpened;
		public event Action HasClosed;

		public bool IsOpen { get; private set; }
		public bool IsInteractable { get { return CanvasGroup.interactable; } set { CanvasGroup.interactable = value; } }

		// PRIVATE MEMBERS

		[TabGroup("Extra Functionality")][SerializeField]
		private bool _canHandleBackAction = true;
		[TabGroup("Extra Functionality")][SerializeField]
		private bool _useSafeArea = true;

		private int _priority;

		private Rect _lastSafeArea;

		private BlurImage _blurImage;
		


		// PUBLIC METHODS

		public void SetPriority(int priority)
		{
			_priority = priority;
		}

		// UIWidget INTERFACE

		protected override void OnInitialize()
		{
			_blurImage = GetComponentInChildren<BlurImage>();
		}

		protected override void OnDeinitialize()
		{
			Close();

			HasOpened = null;
			HasClosed = null;
		}

		public void Tick(float deltaTime)
		{
		}

		public void Open()
		{
			if (IsOpen == true)
				return;
			
			IsOpen = true;

			gameObject.SetActive(true);

			OnOpen();

			RenderBlur();

			if (HasOpened != null)
			{
				HasOpened();
				HasOpened = null;
			}
		}
		
		public void Open(params object[] obj)
		{
			if (IsOpen == true)
				return;
			
			IsOpen = true;

			gameObject.SetActive(true);

			OnOpen(obj);

			RenderBlur();

			if (HasOpened != null)
			{
				HasOpened();
				HasOpened = null;
			}
		}

		public void Close()
		{
			if (IsOpen == false)
				return;

			IsOpen = false;

			OnClose();

			gameObject.SetActive(false);

			ClearBlur();

			if (HasClosed != null)
			{
				HasClosed();
				HasClosed = null;
			}
		}

		public void SetState(bool isOpen)
		{
			if (isOpen == true)
			{
				Open();
			}
			else
			{
				Close();
			}
		}

		public void UpdateSafeArea()
		{
			if (_useSafeArea == false)
				return;

			Rect safeArea = Screen.safeArea;
			if (safeArea == _lastSafeArea)
				return;

			ApplySafeArea(safeArea);
		}

		// IBackHandler INTERFACE

		int IBackHandler.Priority => _priority;
		bool IBackHandler.IsActive => IsOpen == true && _canHandleBackAction;
		bool IBackHandler.OnBackAction() { return OnBackAction(); }

		// UIView INTERFACE

		protected virtual void OnOpen() { }
		protected virtual void OnOpen(params object[] cardInfo) { }
		protected virtual void OnClose() { }
		protected virtual bool OnBackAction()
		{
			if (IsInteractable == true)
			{
				Close();
			}

			return true;
		}

		// PROTECTED METHODS

		protected T Switch<T>() where T : UIView
		{
			Close();

			return GameUI.Open<T>();
		}

		protected T Open<T>() where T : UIView
		{
			return GameUI.Open<T>();
		}

		protected T Open<T>(T view) where T : UIView
		{
			return GameUI.Open<T>(view);
		}
		
		protected T OpenWithBackView<T>(params object[] obj) where T : UICloseView
		{
			return GameUI.OpenWithBackView<T>(this,obj);
		}

		protected T OpenWithBackView<T>() where T : UICloseView
		{
			return GameUI.OpenWithBackView<T>(this);
		}

		protected T OpenWithBackView<T>(UIView backView) where T : UICloseView
		{
			return GameUI.OpenWithBackView<T>(backView);
		}

		// PRIVATE METHODS

		private void ApplySafeArea(Rect safeArea)
		{
			RectTransform rectTransform = transform as RectTransform;

			// Convert safe area rectangle from absolute pixels to normalised anchor coordinates
			Vector2 anchorMin = safeArea.position;
			Vector2 anchorMax = safeArea.position + safeArea.size;
			int screenHeight = Screen.height;
			int screenWidth = Screen.width;
			anchorMin.x /= screenWidth;
			anchorMax.x /= screenWidth;
			anchorMin.y /= screenHeight;
			anchorMax.y /= screenHeight;
			rectTransform.anchorMin = anchorMin;
			rectTransform.anchorMax = anchorMax;

			_lastSafeArea = safeArea;
		}

		private void RenderBlur()
		{
			if (_blurImage == null)
				return;

			CanvasGroup.alpha = 0f;

			int blurDelay = this is IDelayBlurView blurView ? blurView.DelayFrames : 0;
			_blurImage.RenderBlur(this.GetType(), RenderBlurFinished, blurDelay);
		}

		private void RenderBlurFinished()
		{
			CanvasGroup.alpha = 1f;
		}

		private void ClearBlur()
		{
			if (_blurImage == null)
				return;

			_blurImage.ClearBlur(this.GetType());
		}
	}
}
