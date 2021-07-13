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
public class ImmutableAttribute : MultiSupportPropertyAttribute
{
#if UNITY_EDITOR
	public override void BeginDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	{
		EditorGUI.BeginDisabledGroup(true);
	}

	public override void EndDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	{
		EditorGUI.EndDisabledGroup();
	}
#endif
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ImmutableAttribute))]
[CanEditMultipleObjects]
public class ImmutableAttributeDrawer : MultiSupportPropertyAttributeDrawer
{
}
#endif