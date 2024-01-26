using UnityEngine;

namespace MoonKart.UI
{
	public class UIHelpView : UICloseView 
	{
		public const string KEY_HELP_OPENED = "Tutorial.HelpOpened";

		protected override void OnOpen()
		{
			base.OnOpen();

			PersistentStorage.SetBool(KEY_HELP_OPENED, true);
		}
	}
}
