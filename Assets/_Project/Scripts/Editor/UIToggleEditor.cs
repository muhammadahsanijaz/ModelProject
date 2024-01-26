using MoonKart.UI;

namespace MoonKart.Editor
{
	using UnityEditor;
	using UnityEditor.UI;

	[CustomEditor(typeof(UIToggle), true)]
	public class UIToggleEditor : ToggleEditor
	{
		// PRIVATE METHODS


		// ButtonEditor INTERFACE

		protected override void OnEnable()
		{
			base.OnEnable();


		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

		

			serializedObject.ApplyModifiedProperties();
		}
	}
}
