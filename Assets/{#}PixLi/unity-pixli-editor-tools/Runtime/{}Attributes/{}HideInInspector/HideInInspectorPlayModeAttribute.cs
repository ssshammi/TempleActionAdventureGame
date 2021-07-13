using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HideInInspectorPlayModeAttribute : HideInInspectorBaseAttribute
{
#if UNITY_EDITOR
	protected override bool IsHidden()
	{
		return Application.isPlaying;
	}
#endif
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(HideInInspectorPlayModeAttribute))]
[CanEditMultipleObjects]
public class HideInInspectorPlayModeAttributeDrawer : MultiSupportPropertyAttributeDrawer
{
}
#endif