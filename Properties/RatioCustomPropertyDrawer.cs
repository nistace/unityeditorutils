﻿using UnityEditor;
using UnityEngine;
using Utils.Types;

namespace UtilsEditor.Properties {
	[CustomPropertyDrawer(typeof(Ratio))]
	public class RatioCustomPropertyDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, position.height), property.FindPropertyRelative("_value"), GUIContent.none);

			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}
	}
}