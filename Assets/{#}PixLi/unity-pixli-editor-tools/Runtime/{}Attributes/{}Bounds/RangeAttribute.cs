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
public class RangeAttribute : MultiSupportPropertyAttribute
{
	protected float floatMin;
	protected float floatMax;

	protected int intMin;
	protected int intMax;

	private bool _floatRange;

	public RangeAttribute(float min, float max)
	{
		this.floatMin = min;
		this.floatMax = max;

		this._floatRange = true;
	}

	public RangeAttribute(float min, float max, bool drawMain) : base(drawMain)
	{
		this.floatMin = min;
		this.floatMax = max;

		this._floatRange = true;
	}

	public RangeAttribute(int minInt, int maxInt)
	{
		this.intMin = minInt;
		this.intMax = maxInt;

		this._floatRange = false;
	}

	public RangeAttribute(int minInt, int maxInt, bool drawMain) : base(drawMain)
	{
		this.intMin = minInt;
		this.intMax = maxInt;

		this._floatRange = false;
	}

#if UNITY_EDITOR
	public override void MainDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	{
		if (this._floatRange)
			EditorGUI.Slider(rect, serializedProperty, this.floatMin, this.floatMax);
		else
			EditorGUI.IntSlider(rect, serializedProperty, this.intMin, this.intMax);
	}
#endif
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RangeAttribute))]
[CanEditMultipleObjects]
public class RangeAttributeDrawer : MultiSupportPropertyAttributeDrawer
{
}
#endif