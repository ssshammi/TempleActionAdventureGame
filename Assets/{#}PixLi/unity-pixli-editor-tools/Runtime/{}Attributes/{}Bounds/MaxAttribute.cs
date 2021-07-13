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
public class MaxAttribute : MultiSupportPropertyAttribute
{
	private readonly float _floatMax;
	private readonly int _intMax;

	private bool _floatValue;

	public MaxAttribute(float value)
	{
		this._floatMax = value;

		this._floatValue = true;
	}

	public MaxAttribute(float value, bool drawMain) : base(drawMain)
	{
		this._floatMax = value;

		this._floatValue = true;
	}

	public MaxAttribute(int value)
	{
		this._intMax = value;

		this._floatValue = false;
	}

	public MaxAttribute(int value, bool drawMain) : base(drawMain)
	{
		this._intMax = value;

		this._floatValue = false;
	}

#if UNITY_EDITOR
	public override void EndDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	{
		if (this._floatValue)
			serializedProperty.floatValue = Mathf.Clamp(serializedProperty.floatValue, float.MinValue, this._floatMax);
		else
			serializedProperty.intValue = Mathf.Clamp(serializedProperty.intValue, int.MinValue, this._intMax);
	}
#endif
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(MaxAttribute))]
[CanEditMultipleObjects]
public class MaxAttributeDrawer : MultiSupportPropertyAttributeDrawer
{
}
#endif