using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class HideInInspectorBaseAttribute : MultiSupportPropertyAttribute
{
	public HideInInspectorBaseAttribute() : base(true)
	{
	}

	public HideInInspectorBaseAttribute(bool drawMain) : base(drawMain)
	{
	}

#if UNITY_EDITOR
	protected bool? cachedIsHidden;

	/// <summary>
	/// ...
	/// If possible this method should return cached value of `cachedIsHidden` in the best case. But it's not mandatory ofc, just an optimization.
	/// </summary>
	/// <returns></returns>
	protected abstract bool IsHidden();

	public override float GetPropertyHeight()
	{
		return this.IsHidden() ? 0f : EditorGUI.GetPropertyHeight(this.serializedProperty);
	}

	public override void MainDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	{
		if (!this.IsHidden())
			base.MainDrawInInspector(rect, serializedProperty, label);
	}
#endif
}