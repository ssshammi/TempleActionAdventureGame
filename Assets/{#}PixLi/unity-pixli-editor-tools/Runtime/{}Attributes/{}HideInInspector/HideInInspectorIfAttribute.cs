using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HideInInspectorIfAttribute : HideInInspectorBaseAttribute
{
	public string ConditionFieldName;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="conditionFieldName">Name of the condition field</param>
	public HideInInspectorIfAttribute(string conditionFieldName)
	{
		this.ConditionFieldName = conditionFieldName;
	}

#if UNITY_EDITOR
	protected override bool IsHidden()
	{
		// Find that property by the path.
		SerializedProperty attributeConditionField = this.serializedProperty.serializedObject.FindProperty(this.ConditionFieldName);

		// If the field was found return it's boolean value.
		if (attributeConditionField != null && attributeConditionField.propertyType == SerializedPropertyType.Boolean)
			return !attributeConditionField.boolValue;

		Debug.LogWarning(this.ConditionFieldName + " is invalid condition(not supported type) or not a condition at all!");

		return false;
	}
#endif
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(HideInInspectorIfAttribute))]
[CanEditMultipleObjects]
public class HideInInspectorIfAttributeDrawer : MultiSupportPropertyAttributeDrawer
{
}
#endif