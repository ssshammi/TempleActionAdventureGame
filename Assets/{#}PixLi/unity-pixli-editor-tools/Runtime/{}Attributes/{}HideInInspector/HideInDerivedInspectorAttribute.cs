using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HideInDerivedInspectorAttribute : HideInInspectorBaseAttribute
{
#if UNITY_EDITOR
	public readonly Type _DerivedType = null;

	protected override bool IsHidden()
	{
		if (!this.cachedIsHidden.HasValue)
		{
			string[] fieldNames = this.serializedProperty.propertyPath.Split('.');

			// If it's not a nested field
			if (fieldNames.Length <= 1)
			{
				if (this._DerivedType != null)
					this.cachedIsHidden = this.serializedProperty.serializedObject.targetObject.GetType() == this._DerivedType;
				else
					this.cachedIsHidden = this.serializedProperty.serializedObject.targetObject.GetType() != this.serializedProperty.GetContainingObjectFieldInfo().DeclaringType;
			}
			else
			{
				if (this._DerivedType != null)
					this.cachedIsHidden = this.serializedProperty.GetParentObjectFieldType(fieldNames) == this._DerivedType;
				else
					this.cachedIsHidden = this.serializedProperty.GetParentObjectFieldType(fieldNames) != this.serializedProperty.GetContainingObjectFieldInfo().DeclaringType;
			}

		}

		return this.cachedIsHidden.Value;
	}

	/// <summary>
	/// Hide from specific decended type
	/// </summary>
	/// <param name="derivedType"></param>
	public HideInDerivedInspectorAttribute(Type derivedType) : base()
	{
		this._DerivedType = derivedType;
	}

	/// <summary>
	/// Hide from all descended types
	/// </summary>
	public HideInDerivedInspectorAttribute() : this(null)
	{
	}
#endif
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(HideInDerivedInspectorAttribute))]
[CanEditMultipleObjects]
public class HideInDerivedInspectorAttributeDrawer : MultiSupportPropertyAttributeDrawer
{
}
#endif