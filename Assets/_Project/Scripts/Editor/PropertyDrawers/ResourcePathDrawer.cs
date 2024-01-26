using UnityEngine;
using UnityEditor;
using System;

namespace MoonKart
{
	[CustomPropertyDrawer(typeof(SpritePath))]
	[CustomPropertyDrawer(typeof(ResourcePath))]
	[CustomPropertyDrawer(typeof(ResourcePathAttribute))]
	public class ResourcePathDrawer : PropertyDrawer
	{
		// CONSTANTS

		private const string RESOURCES_PREFIX = "Resources/";

		// PRIVATE MEMBERS

		private UnityEngine.Object _unityObj;
		private Type _type;
		private string _path;
		private SerializedProperty _pathProp;
		private bool _error;

		private GUIContent _pathLabel;
		private GUIStyle _pathStyle;

		// PropertyDrawer INTERFACE

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			Prepare(property);

			EditorGUI.BeginProperty(position, label, property);
			EditorGUI.BeginChangeCheck();

			position = EditorGUI.PrefixLabel(position, label);

			using (new ErrorContent(_error))
			{
				_unityObj = EditorGUI.ObjectField(position, _unityObj, _type, false);
			}

			if (EditorGUI.EndChangeCheck() == true)
			{
				OnChange();
			}

			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight;
		}

		private void Prepare(SerializedProperty property)
		{
			Type fieldType = fieldInfo.FieldType;
			if (fieldType == typeof(string))
			{
				_type = (attribute as ResourcePathAttribute).Type;
			}
			else
			{
				if (property.type == nameof(SpritePath))
				{
					_type = typeof(Sprite);
				}
				else if (property.type == nameof(ResourcePath))
				{
					_type = typeof(GameObject);
				}
				else
				{
					throw new NotImplementedException();
				}

				property = property.FindPropertyRelative("_path");
			}

			_pathLabel = _pathLabel ?? new GUIContent("?");
			_pathProp = property;
			_path = property.stringValue;
			_unityObj = Resources.Load(_path, _type);

			if (_unityObj != null)
			{
				_error = false;
				_pathLabel.tooltip = AssetDatabase.GetAssetPath(_unityObj);
			}
			else
			{
				_error = string.IsNullOrEmpty(_path) == false;
				_pathLabel.tooltip = _path;
			}

			_pathStyle = new GUIStyle(EditorStyles.label);
			_pathStyle.alignment = TextAnchor.MiddleRight;
			_pathStyle.fontStyle = FontStyle.Bold;
			_pathStyle.font = EditorStyles.standardFont;
		}

		private void OnChange()
		{
			if (_unityObj != null)
			{
				_path = AssetDatabase.GetAssetPath(_unityObj);
				_pathLabel.tooltip = _path;
				_error = _path.Contains(RESOURCES_PREFIX) == false;

				if (_error == false)
				{
					_path = _path.Substring(_path.IndexOf(RESOURCES_PREFIX) + RESOURCES_PREFIX.Length);
					_path = System.IO.Path.ChangeExtension(_path, null);
				}
			}
			else
			{
				_path = string.Empty;
				_pathLabel.tooltip = _path;
				_error = false;
			}

			_pathProp.stringValue = _path;
		}

		private class ErrorContent : IDisposable
		{
			private static readonly Color ERROR_COLOR = new Color(1f, 0.3f, 0.3f, 1f);

			private Color _cachedColor;

			public ErrorContent(bool error)
			{
				_cachedColor = GUI.backgroundColor;

				if (error == true)
				{
					GUI.backgroundColor = ERROR_COLOR;
				}
			}

			void IDisposable.Dispose()
			{
				GUI.backgroundColor = _cachedColor;
			}
		}
	}
}
