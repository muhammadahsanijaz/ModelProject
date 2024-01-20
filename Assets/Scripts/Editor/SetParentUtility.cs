using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MoonKart.Editor
{
	public class SetParentUtility : EditorWindow
	{
		// PRIVATE MEMBERS

		[SerializeField]
		private Transform _parent;

		// PUBLIC METHODS

		[MenuItem("Racing/Set Parent Utility")]
		public static void OpenWindow()
		{
			SetParentUtility window = (SetParentUtility)EditorWindow.GetWindow(typeof(SetParentUtility));
			window.name = "Set Parent";
			window.Show();
		}

		// MONOBEHAVIOR

		private void OnGUI()
		{
			_parent = EditorGUILayout.ObjectField("Parent", _parent, typeof(Transform), true) as Transform;

			GUI.enabled = _parent != null;

			if(GUILayout.Button("Assign Selection to Parent"))
			{
				var selectedObjects = Selection.objects.OfType<GameObject>();

				foreach (var selectedObject in selectedObjects)
				{
					Undo.SetTransformParent(selectedObject.transform, _parent, "Assign Selection to Parent");
				}
			}
		}
	}
}
