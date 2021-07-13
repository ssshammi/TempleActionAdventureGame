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

using Object = UnityEngine.Object;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
[Serializable]
public class ButtonAttribute : MultiSupportAttribute
{
	protected GUIContent label;

	public ButtonAttribute()
	{
	}

	protected float height = 20f;

	public ButtonAttribute(float height)
	{
		this.height = height;
	}

	public ButtonAttribute(string labelText)
	{
		this.label = new GUIContent(labelText);
	}

#if UNITY_EDITOR
	protected MethodInfo methodInfo;

	public override void Initialize(Object target, SerializedObject serializedObject, MemberInfo memberInfo)
	{
		base.Initialize(target, serializedObject, memberInfo);

		this.methodInfo = this.memberInfo as MethodInfo;
			
		if (this.label == null)
			this.label = new GUIContent(this.methodInfo.Name.ToDisplayValue());
	}

	public override void MainDrawInInspector()
	{
		if (GUILayout.Button(this.label, GUILayout.Height(this.height)))
			this.methodInfo.Invoke(this.target, null);
	}
#endif
}