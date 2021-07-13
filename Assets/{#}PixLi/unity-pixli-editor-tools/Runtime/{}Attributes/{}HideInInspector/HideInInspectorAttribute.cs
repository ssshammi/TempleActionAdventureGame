using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HideInInspector : HideInInspectorBaseAttribute
{
#if UNITY_EDITOR
	public override float GetPropertyHeight()
	{
		return 0f;
	}

	protected override bool IsHidden()
	{
		return true;
	}
#endif
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(HideInInspector))]
[CanEditMultipleObjects]
public class HideInInspectorDrawer : MultiSupportPropertyAttributeDrawer
{
}
#endif