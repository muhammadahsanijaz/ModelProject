using UnityEngine;
using TMPro;

namespace MoonKart.UI
{
	public class UIChangeNicknameView : UICloseView, IDelayBlurView
	{
		// PRIVATE MEMBERS

		[SerializeField]
		private TextMeshProUGUI _caption;
		[SerializeField]
		private TMP_InputField _name;
		[SerializeField]
		private UIButton _confirmButton;
		[SerializeField]
		private int _minCharacters = 5;

		// PUBLIC METHODS

		public void SetData(string caption, bool nameRequired)
		{
			_caption.text = caption;
			CloseButton.SetActive(nameRequired == false);
		}

		// UIView INTERFACE

		protected override void OnInitialize()
		{
			base.OnInitialize();

			_confirmButton.onClick.AddListener(OnConfirmButton);
		}

		protected override void OnDeinitialize()
		{
			_confirmButton.onClick.RemoveListener(OnConfirmButton);

			base.OnDeinitialize();
		}

		protected override void OnOpen()
		{
			base.OnOpen();

			string currentNickname = Context.Player.Nickname;
			if (currentNickname.HasValue() == false)
			{
				_name.text = "Player" + Random.Range(10000, 100000);
			}
			else
			{
				_name.text = Context.Player.Nickname;
			}
		}

		protected override void OnTick()
		{
			base.OnTick();

			_confirmButton.interactable = _name.text.Length >= _minCharacters && _name.text != Context.Player.Nickname;
		}

		// IDelayBlurView INTERFACE

		int IDelayBlurView.DelayFrames => Context.Player.Nickname.HasValue() == false ? 2 : 0;

		// PRIVATE METHODS

		private void OnConfirmButton()
		{
			(Context.Player as PlayerData).Nickname = _name.text;
			Close();
		}
	}
}
