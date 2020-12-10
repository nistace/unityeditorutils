using System.Linq;
using UnityEditor;
using UnityEngine;
using Utils.Id;

namespace UtilsEditor.Properties {
	[CustomPropertyDrawer(typeof(DataId))]
	public class DataIdCustomPropertyDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			var w6 = position.width / 6;

			EditorGUI.PropertyField(new Rect(position.x, position.y, w6, position.height), property.FindPropertyRelative("_value"), GUIContent.none);

			if (property.FindPropertyRelative("_value").intValue == 0 || GUI.Button(new Rect(position.x + w6, position.y, 4 * w6, position.height), "Next")) {
				property.FindPropertyRelative("_value").intValue = DataId.GetNextId();
			}

			if (property.FindPropertyRelative("_value").intValue == 0 || GUI.Button(new Rect(position.x + 5 * w6, position.y, w6, position.height), "Check")) {
				var identities = Resources.LoadAll<DataMonoBehaviour>("").Cast<IData>().Union(Resources.LoadAll<DataScriptableObject>("")).ToList();
				identities.Sort((t, u) => t.id - u.id);
				var countErrors = 0;
				for (var i = 0; i < identities.Count - 1; ++i) {
					if (identities[i].id != identities[i + 1].id) continue;
					countErrors++;
					Debug.LogError("Two objects with the same id: " + identities[i] + " and " + identities[i + 1] + ". ID " + identities[i].id);
				}
				if (countErrors == 0) Debug.Log("No ID error found.");
				else Debug.LogWarning(countErrors + " errors found.");
			}

			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}
	}
}