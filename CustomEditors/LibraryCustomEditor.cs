using UnityEditor;
using UnityEngine;

namespace UtilsEditor {
	public abstract class LibraryCustomEditor<E> : Editor {
		public override void OnInspectorGUI() {
			var library = (Library<E>) target;
			var itemsCount = Mathf.Min(serializedObject.FindProperty("_itemNames").arraySize, serializedObject.FindProperty("_items").arraySize);
			GUI.enabled = false;
			EditorGUILayout.ObjectField("Script:", MonoScript.FromScriptableObject(library), typeof(Library<E>), false);
			GUI.enabled = true;

			EditorGUILayout.PropertyField(serializedObject.FindProperty("_id"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("_defaultItem"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("_orderIndex"));
			AddOtherFields();
			GUILayout.Label("Items:");
			for (var i = 0; i < itemsCount; ++i) {
				GUILayout.BeginHorizontal();
				EditorGUILayout.PropertyField(serializedObject.FindProperty("_itemNames").GetArrayElementAtIndex(i), GUIContent.none);
				EditorGUILayout.PropertyField(serializedObject.FindProperty("_items").GetArrayElementAtIndex(i), GUIContent.none);
				if (GUILayout.Button("Copy", GUILayout.Width(50))) {
					serializedObject.FindProperty("_itemNames").InsertArrayElementAtIndex(i + 1);
					serializedObject.FindProperty("_itemNames").GetArrayElementAtIndex(i + 1).stringValue = serializedObject.FindProperty("_itemNames").GetArrayElementAtIndex(i).stringValue + ".copy";
					serializedObject.FindProperty("_items").InsertArrayElementAtIndex(i + 1);
					CopyValue(serializedObject.FindProperty("_items").GetArrayElementAtIndex(i), serializedObject.FindProperty("_items").GetArrayElementAtIndex(i + 1));
				}
				if (GUILayout.Button("-", GUILayout.Width(30))) {
					serializedObject.FindProperty("_itemNames").DeleteArrayElementAtIndex(i);
					serializedObject.FindProperty("_items").DeleteArrayElementAtIndex(i);
					itemsCount--;
					i--;
				}

				GUILayout.EndHorizontal();
			}

			if (GUILayout.Button("+")) {
				if (serializedObject.FindProperty("_itemNames").arraySize == itemsCount)
					serializedObject.FindProperty("_itemNames").InsertArrayElementAtIndex(itemsCount);
				if (serializedObject.FindProperty("_items").arraySize == itemsCount)
					serializedObject.FindProperty("_items").InsertArrayElementAtIndex(itemsCount);
			}
			if (GUILayout.Button("Sort alphabetically")) {
				library.SortItems();
			}
			if (GUILayout.Button("Reload")) {
				library.Load();
			}
			serializedObject.ApplyModifiedProperties();
		}

		protected abstract void CopyValue(SerializedProperty origin, SerializedProperty to);

		public virtual void AddOtherFields() { }
	}

	[CustomEditor(typeof(ColorLibrary))]
	public class ColorLibraryCustomEditor : LibraryCustomEditor<Color> {
		protected override void CopyValue(SerializedProperty origin, SerializedProperty to) => to.colorValue = origin.colorValue;
	}

	[CustomEditor(typeof(SpriteLibrary))]
	public class SpriteLibraryCustomEditor : LibraryCustomEditor<Sprite> {
		protected override void CopyValue(SerializedProperty origin, SerializedProperty to) => to.objectReferenceValue = origin.objectReferenceValue;
	}

	[CustomEditor(typeof(ConstantLibrary))]
	public class ConstantLibraryCustomEditor : LibraryCustomEditor<string> {
		protected override void CopyValue(SerializedProperty origin, SerializedProperty to) => to.stringValue = origin.stringValue;
	}

	[CustomEditor(typeof(NetworkPrefabsLibrary))]
	public class NetworkPrefabsLibraryCustomEditor : LibraryCustomEditor<GameObject> {
		protected override void CopyValue(SerializedProperty origin, SerializedProperty to) => to.objectReferenceValue = origin.objectReferenceValue;
	}
}