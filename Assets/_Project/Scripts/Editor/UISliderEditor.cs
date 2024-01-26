using MoonKart.UI;

namespace MoonKart.Editor
{
	using UnityEditor;
	using UnityEditor.UI;

	[CustomEditor(typeof(UISlider), true)]
	public class UISliderEditor : SliderEditor
	{
		// PRIVATE METHODS

		private SerializedProperty _valueText;
		private SerializedProperty _maxValueText;
		private SerializedProperty _valueFormat;
		private SerializedProperty _slashText;


		// ButtonEditor INTERFACE

		protected override void OnEnable()
		{
			base.OnEnable();

			_valueText = serializedObject.FindProperty("_valueText");
			_maxValueText = serializedObject.FindProperty("_maxValueText");
			_slashText = serializedObject.FindProperty("_slashText");
			_valueFormat = serializedObject.FindProperty("_valueFormat");

		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			EditorGUILayout.PropertyField(_valueText);
			EditorGUILayout.PropertyField(_maxValueText);
			EditorGUILayout.PropertyField(_slashText);
			EditorGUILayout.PropertyField(_valueFormat);


			serializedObject.ApplyModifiedProperties();
		}
	}
}
