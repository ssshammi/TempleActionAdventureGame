using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Object = UnityEngine.Object;

[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
public abstract class MultiSupportAttribute : Attribute
{
#if UNITY_EDITOR
	protected Object target;
	protected SerializedObject serializedObject;
	protected MemberInfo memberInfo;

	public virtual void Initialize(Object target, SerializedObject serializedObject, MemberInfo memberInfo)
	{
		this.target = target;
		this.serializedObject = serializedObject;
		this.memberInfo = memberInfo;
	}

	public virtual float GetHeight() => 0f;

	public virtual void BeginDrawInInspector() { }

	public virtual void LeftDrawInInspector() { }
	public virtual void MainDrawInInspector() { }
	public virtual void RightDrawInInspector() { }

	public virtual void DrawInInspector() { }

	public virtual void EndDrawInInspector() { }
#endif
}