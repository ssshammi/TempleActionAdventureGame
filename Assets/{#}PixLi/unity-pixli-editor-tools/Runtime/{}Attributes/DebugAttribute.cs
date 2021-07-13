using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class DebugAttribute : MultiSupportPropertyAttribute
{
	public enum DebugType { Full, Visible }

	private DebugType _debugType;
	private int _depth;

	public DebugAttribute(DebugType debugType = DebugType.Full, int depth = 1) : base(true)
	{
		this._debugType = debugType;
		this._depth = depth;
	}

#if UNITY_EDITOR
	public static void PropertyField(SerializedProperty serializedProperty, bool includeChildren, DebugType debugType, int depth)
	{
		IEnumerable<SerializedProperty> children;

		switch (debugType)
		{
			case DebugType.Full:

				children = serializedProperty.GetChildren(depth);

				break;
			case DebugType.Visible:

				children = serializedProperty.GetVisibleChildren(depth);

				break;
			default:
				throw new NotImplementedException("Didn't expect this to ever happen, but apparently _debugType was " +
					"assigned some enum type that switch statement doesn't support at the moment.");
		}

		EditorGUILayout.BeginVertical();
		using (EditorGUI.DisabledGroupScope disabledGroupScope = new EditorGUI.DisabledGroupScope(true))
		{
			serializedProperty.isExpanded = EditorGUILayout.Foldout(
				serializedProperty.isExpanded,
				serializedProperty.displayName,
				true
			);

			if (serializedProperty.isExpanded)
				using (EditorGUI.IndentLevelScope indentLevelScope = new EditorGUI.IndentLevelScope())
					foreach (SerializedProperty child in children)
						EditorGUILayout.PropertyField(child, includeChildren);
		}
		EditorGUILayout.EndVertical();
	}

	public override float GetPropertyHeight() => 0f;

	public override void MainDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	{
		DebugAttribute.PropertyField(this.serializedProperty, true, this._debugType, this._depth);
	}

	[CustomPropertyDrawer(typeof(DebugAttribute))]
	[CanEditMultipleObjects]
	public class DebugAttributeDrawer : MultiSupportPropertyAttributeDrawer
	{
	}
#endif
}