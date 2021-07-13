using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

public class ReorderableListAttribute : MultiSupportPropertyAttribute
{
#if UNITY_EDITOR
	private ReorderableList _reorderableList;

	private void DrawHeader(Rect rect)
	{
		EditorGUI.LabelField(rect, new GUIContent(this.serializedProperty.displayName));
	}

	private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
	{
		EditorGUI.indentLevel++;

		EditorGUI.LabelField(new Rect(new Vector2(rect.position.x - EditorGUIUtility.singleLineHeight, rect.position.y), rect.size), new GUIContent(index.ToString()));

		EditorGUI.indentLevel++;

		SerializedProperty element = this._reorderableList.serializedProperty.GetArrayElementAtIndex(index);

		Object firstObjectReferenceValue = null;

		//FieldInfo[] elementFields = ((object[])element.GetValueOfExternal(this.serializedProperty))[index].GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

		//for (int i = 0; i < elementFields.Length; i++)
		//{
		//	SerializedProperty elementFieldSerializedProperty = element.FindPropertyRelative(elementFields[i].Name);
		//	if (elementFieldSerializedProperty.objectReferenceValue != null)
		//	{
		//		firstObjectReferenceValue = elementFieldSerializedProperty.objectReferenceValue;
		//		break;
		//	}
		//}

		EditorGUI.PropertyField(rect, element, new GUIContent(firstObjectReferenceValue != null ? firstObjectReferenceValue.name : "Element " + index), true);

		EditorGUI.indentLevel -= 2;
	}

	private float GetElementHeight(int index)
	{
		SerializedProperty element = this._reorderableList.serializedProperty.GetArrayElementAtIndex(index);

		float propertyExtensionHeight = 0f;

		//FieldInfo[] elementFields = ((object[])element.GetValueOfExternal(this.serializedProperty))[index].GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

		//for (int i = 0; i < elementFields.Length; i++)
		//{
		//	SerializedProperty elementFieldSerializedProperty = element.FindPropertyRelative(elementFields[i].Name);

		//	object[] attributes = elementFields[i].GetCustomAttributes(false);

		//	for (int j = 0; j < attributes.Length; j++)
		//	{
		//		MultiSupportPropertyAttribute multiSupportPropertyAttribute = attributes[j] as MultiSupportPropertyAttribute;

		//		if (multiSupportPropertyAttribute != null)
		//		{
		//			if (element.isExpanded)
		//			{
		//				multiSupportPropertyAttribute.Initialize(elementFieldSerializedProperty);
		//				propertyExtensionHeight += multiSupportPropertyAttribute.GetPropertyExtensionHeight();
		//			}
		//		}
		//	}
		//}

		return EditorGUIUtility.singleLineHeight * 2f + propertyExtensionHeight;
	}

	public override void Initialize(SerializedProperty serializedProperty)
	{
		base.Initialize(serializedProperty);

		this._reorderableList = new ReorderableList(serializedProperty.serializedObject, serializedProperty.FindPropertyRelative("Value"), true, true, true, true)
		{
			drawHeaderCallback = this.DrawHeader,
			drawElementCallback = this.DrawElement,
			elementHeightCallback = this.GetElementHeight
		};
	}

	public override void MainDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	{
		this._reorderableList.DoLayoutList();
	}
#endif
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReorderableListAttribute))]
[CanEditMultipleObjects]
public class ReorderableListAttributeDrawer : MultiSupportPropertyAttributeDrawer
{
}
#endif