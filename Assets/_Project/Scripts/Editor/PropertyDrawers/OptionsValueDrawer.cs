using UnityEditor;
using UnityEngine;

namespace MoonKart.Editor
{
	[CustomPropertyDrawer(typeof(OptionsValue))]
	public sealed class OptionsValueDrawer : PropertyDrawer
	{
		// PRIVATE MEMBERS

		private float _height;

		// PropertyDrawer INTERFACE

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			_height = 0.0f;

			SerializedProperty keyProperty  = property.FindPropertyRelative("Key");
			SerializedProperty typeProperty = property.FindPropertyRelative("Type");

			property.isExpanded = EditorGUI.Foldout(NextPropertyPosition(position), property.isExpanded, keyProperty.stringValue);

			if (property.isExpanded == false)
				return;

			property.serializedObject.Update();

			EditorGUI.BeginChangeCheck();

			EditorGUI.PropertyField(NextPropertyPosition(position), keyProperty);
			EditorGUI.PropertyField(NextPropertyPosition(position), typeProperty);

			EOptionsValueType type = (EOptionsValueType)typeProperty.enumValueIndex;

			switch (type)
			{
				case EOptionsValueType.Bool:
					EditorGUI.PropertyField(NextPropertyPosition(position), property.FindPropertyRelative("BoolValue"));
					break;
				case EOptionsValueType.Float:
					EditorGUI.PropertyField(NextPropertyPosition(position), property.FindPropertyRelative("FloatValue.Value"));
					EditorGUI.PropertyField(NextPropertyPosition(position), property.FindPropertyRelative("FloatValue.MinValue"));
					EditorGUI.PropertyField(NextPropertyPosition(position), property.FindPropertyRelative("FloatValue.MaxValue"));
					break;
				case EOptionsValueType.Int:
					EditorGUI.PropertyField(NextPropertyPosition(position), property.FindPropertyRelative("IntValue.Value"));
					EditorGUI.PropertyField(NextPropertyPosition(position), property.FindPropertyRelative("IntValue.MinValue"));
					EditorGUI.PropertyField(NextPropertyPosition(position), property.FindPropertyRelative("IntValue.MaxValue"));
					break;
				case EOptionsValueType.String:
					EditorGUI.PropertyField(NextPropertyPosition(position), property.FindPropertyRelative("StringValue"));
					break;
			}

			if (EditorGUI.EndChangeCheck() == true)
			{
				property.serializedObject.ApplyModifiedProperties();
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			float lineHeight = GetLineHeight();

			if (property.isExpanded == false)
				return lineHeight;

			float height = lineHeight * 3.0f;

			EOptionsValueType type = (EOptionsValueType)property.FindPropertyRelative("Type").enumValueIndex;

			switch (type)
			{
				case EOptionsValueType.Bool:
				case EOptionsValueType.String:
					height += lineHeight;
					break;
				case EOptionsValueType.Float:
				case EOptionsValueType.Int:
					height += lineHeight * 3.0f;
					break;
			}

			return height;
		}

		private Rect NextPropertyPosition(Rect position)
		{
			position.height = GetLineHeight();
			position.center = new Vector2(position.center.x, position.center.y + _height);

			_height += position.height;

			return position;
		}

		private float GetLineHeight()
		{
			return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
		}
	}
}
