using UnityEngine;
using DG.Tweening;

namespace MoonKart.UI
{
	public class UIGameplayMenuView : UIView 
	{
		// PRIVATE MEMBERS

		[SerializeField]
		private UIBehaviour _menu;
		[SerializeField]
		private UIButton _menuButton;
		[SerializeField]
		private UIButton _leaveRaceButton;
		[SerializeField]
		private UIButton _continueButton;
		[SerializeField]
		private UIButton _backButton;
		[SerializeField]
		private UIButton _closeAreaButton;

		[Header("Animation")]
		[SerializeField]
		private float _hideMenuDuration = 0.3f;

		// UIView INTERFACE

		protected override void OnInitialize()
		{
			base.OnInitialize();

			_menuButton.onClick.AddListener(OnMenuButton);
			_leaveRaceButton.onClick.AddListener(OnLeaveRaceButton);
			
			_continueButton.onClick.AddListener(OnContinueButton);
			_backButton.onClick.AddListener(OnContinueButton);
			_closeAreaButton.onClick.AddListener(OnContinueButton);

			HideMenu(true);
		}

		protected override void OnDeinitialize()
		{
			_menuButton.onClick.RemoveListener(OnMenuButton);
			_leaveRaceButton.onClick.RemoveListener(OnLeaveRaceButton);
			_continueButton.onClick.RemoveListener(OnContinueButton);
			_backButton.onClick.RemoveListener(OnContinueButton);
			_closeAreaButton.onClick.RemoveListener(OnContinueButton);

			base.OnDeinitialize();
		}

		protected override void OnClose()
		{
			HideMenu(true);

			DOTween.Complete(_menu.RectTransform);
			DOTween.Kill(_menu.RectTransform);

			base.OnClose();
		}

		protected override bool OnBackAction()
		{
			if (IsInteractable == false)
				return false;

			if (_menuButton.IsActive() == true)
			{
				ShowMenu();
			}
			else
			{
				HideMenu();
			}

			return true;
		}

		// PRIVATE METHODS

		private void ShowMenu(bool immediate = false)
		{
			SetMenuPosition(0, immediate == true ? 0f : _hideMenuDuration);

			_closeAreaButton.SetActive(true);
			_menuButton.SetActive(false);
		}

		private void HideMenu(bool immediate = false)
		{
			SetMenuPosition(-800, immediate == true ? 0f : _hideMenuDuration);

			_closeAreaButton.SetActive(false);
			_menuButton.SetActive(true);
		}

		private void OnMenuButton()
		{
			ShowMenu();
		}

		private void OnLeaveRaceButton()
		{
			var dialogView = Open<UIYesNoDialogView>();
			dialogView.Title.text = "Leave Race";
			dialogView.Description.text = "Are you sure you want to leave this race?";
			dialogView.YesButtonText.text = "Leave";

			dialogView.HasClosed += (result) =>
			{
				if (result == true)
				{
					(GameUI.Game as Gameplay).LoadMainMenu();
				}
			};
		}

		private void OnSettingsButton()
		{
			HideMenu(true);
			Open<UISettingsView>();
		}

		private void OnContinueButton()
		{
			HideMenu();
		}

		private void SetMenuPosition(float anchoredPositionX, float time)
		{
			var menuTransform = _menu.RectTransform;

			DOTween.Kill(menuTransform);

			if (time == 0f)
			{
				var tempPosition = menuTransform.anchoredPosition;
				tempPosition.x = anchoredPositionX;
				menuTransform.anchoredPosition = tempPosition;
			}
			else
			{
				menuTransform.DOAnchorPosX(anchoredPositionX, _hideMenuDuration);
				menuTransform.DOPlay();
			}
		}
	}
}
