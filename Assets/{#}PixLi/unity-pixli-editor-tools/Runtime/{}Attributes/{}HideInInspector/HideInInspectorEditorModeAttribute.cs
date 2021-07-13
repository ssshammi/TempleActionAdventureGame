using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HideInInspectorEditorModeAttribute : HideInInspectorBaseAttribute
{
#if UNITY_EDITOR
	protected override bool IsHidden()
	{
		return !Application.isPlaying;
	}
#endif
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(HideInInspectorEditorModeAttribute))]
[CanEditMultipleObjects]
public class HideInInspectorEditorModeAttributeDrawer : MultiSupportPropertyAttributeDrawer
{
}
#endif