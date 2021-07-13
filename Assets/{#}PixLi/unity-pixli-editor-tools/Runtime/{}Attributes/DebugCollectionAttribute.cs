using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class DebugCollectionAttribute : MultiSupportAttribute
{
	private DebugAttribute.DebugType _debugType;

	public DebugCollectionAttribute(DebugAttribute.DebugType debugType = DebugAttribute.DebugType.Full)
	{
		this._debugType = debugType;
	}

#if UNITY_EDITOR
	private SerializedProperty _serializedProperty;

	public override void Initialize(UnityEngine.Object target, SerializedObject serializedObject, MemberInfo memberInfo)
	{
		base.Initialize(target, serializedObject, memberInfo);

		FieldInfo fieldInfo = memberInfo as FieldInfo;
		if (fieldInfo != null &&
			typeof(ICollection).IsAssignableFrom(fieldInfo.FieldType)) // Check if it's a collection, discard string as collection.
		{
			this._serializedProperty = serializedObject.FindProperty(memberInfo.Name);
		}

	}

	public override void MainDrawInInspector()
	{
		//? Bad idea, if this doesn't work then better throw an exception to let know.
		//if (this._serializedProperty == null)
		//	return;

		IEnumerable<SerializedProperty> children;

		switch (this._debugType)
		{
			case DebugAttribute.DebugType.Full:

				children = this._serializedProperty.GetChildren();

				break;
			case DebugAttribute.DebugType.Visible:

				children = this._serializedProperty.GetVisibleChildren();

				break;
			default:
				throw new NotImplementedException("Didn't expect this to ever happen, but apparently _debugType was " +
					"assigned some enum type that switch statement doesn't support at the moment.");
		}

		EditorGUILayout.BeginVertical();
		using (EditorGUI.DisabledGroupScope disabledGroupScope = new EditorGUI.DisabledGroupScope(true))
		{
			this._serializedProperty.isExpanded = EditorGUILayout.Foldout(
				this._serializedProperty.isExpanded,
				this._serializedProperty.displayName,
				true
			);

			if (this._serializedProperty.isExpanded)
			{
				using (EditorGUI.IndentLevelScope indentLevelScope = new EditorGUI.IndentLevelScope())
				{
					foreach (SerializedProperty child in children)
					{
						EditorGUILayout.PropertyField(child, true);
					}

					//using (IEnumerator<SerializedProperty> iterator = this._serializedProperty.GetChildren().GetEnumerator())
					//{
					//	while (iterator.MoveNext())
					//	{
					//		EditorGUILayout.PropertyField(iterator.Current, true);
					//	}
					//}
				}
			}
		}
		EditorGUILayout.EndVertical();
	}
#endif
}