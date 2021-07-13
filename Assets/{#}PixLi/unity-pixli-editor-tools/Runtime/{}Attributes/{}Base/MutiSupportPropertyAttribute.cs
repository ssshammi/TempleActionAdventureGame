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
[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public abstract class MultiSupportPropertyAttribute : PropertyAttribute
{
	public bool DrawMain_ { get; private set; } = false;

	public MultiSupportPropertyAttribute()
	{
	}

	public MultiSupportPropertyAttribute(bool drawMain)
	{
		this.DrawMain_ = drawMain;
	}

#if UNITY_EDITOR
	protected SerializedProperty serializedProperty;
	public SerializedProperty _SerializedProperty => this.serializedProperty;

	public float CalculatedPropertyExtensionWidth { get; set; }

	public virtual void Initialize(SerializedProperty serializedProperty) =>
		this.serializedProperty = serializedProperty ?? throw new ArgumentNullException(nameof(serializedProperty));

	public virtual float GetPropertyHeight() => EditorGUI.GetPropertyHeight(this.serializedProperty);
	public virtual float GetPropertyExtensionWidth() => 0f;
	public virtual float GetPropertyExtensionHeight() => 0f;

	public virtual void BeginDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label) { }

	public virtual void LeftDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label) { }
	public virtual void MainDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label) => EditorGUI.PropertyField(rect, serializedProperty, true);
	public virtual void DrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label) { }
	public virtual void RightDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label) { }

	public virtual void EndDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label) { }
#endif
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(MultiSupportPropertyAttribute))]
[CanEditMultipleObjects]
public class MultiSupportPropertyAttributeDrawer : PropertyDrawer
{
	private List<MultiSupportPropertyAttribute> _attributes = new List<MultiSupportPropertyAttribute>();
	private int _mainAttributeIndex;

	public sealed override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
	{
		if (this._attributes.Count < 1)
		{
			object[] attributes = this.fieldInfo.GetCustomAttributes(true);

			for (int a = 0; a < attributes.Length; a++)
			{
				if (attributes[a] is MultiSupportPropertyAttribute multiSupportPropertyAttribute)
				{
					multiSupportPropertyAttribute.Initialize(serializedProperty);

					if (multiSupportPropertyAttribute.DrawMain_)
						this._mainAttributeIndex = this._attributes.Count;

					this._attributes.Add(multiSupportPropertyAttribute);
				}
			}

			//! Can be simplified with Linq
			float propertyExtensionWidth = 0;

			for (int a = 0; a < this._attributes.Count; a++)
				propertyExtensionWidth += this._attributes[a].GetPropertyExtensionWidth();

			for (int a = 0; a < this._attributes.Count; a++)
				this._attributes[a].CalculatedPropertyExtensionWidth = propertyExtensionWidth;
		}

		//! Can be simplified with Linq
		float propertyExtensionHeight = 0;

		for (int a = 0; a < this._attributes.Count; a++)
			propertyExtensionHeight += this._attributes[a].GetPropertyExtensionHeight();

		return this._attributes[this._mainAttributeIndex].GetPropertyHeight() + propertyExtensionHeight;
	}

	private MethodInfo _onScopeChanged;

	public sealed override void OnGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	{
		//serializedProperty.serializedObject.Update(); - breaks everything

		using (EditorGUI.ChangeCheckScope changeCheckScope = new EditorGUI.ChangeCheckScope())
		{
			//! This is not the same as EditorGUILayout.BeginVertical(); . Scope will enclose everything inside, and combine elements in it. While EditorGUILayout.BeginVertical(); will layout them in a proper way.
			//using (EditorGUILayout.VerticalScope verticalScope = new EditorGUILayout.VerticalScope()) 
			EditorGUILayout.BeginVertical();
			{
				for (int a = 0; a < this._attributes.Count; a++)
					this._attributes[a].BeginDrawInInspector(rect, serializedProperty, label);

				//using (EditorGUILayout.HorizontalScope horizontalScope = new EditorGUILayout.HorizontalScope())
				EditorGUILayout.BeginHorizontal();
				{
					for (int a = 0; a < this._attributes.Count; a++)
						this._attributes[a].LeftDrawInInspector(rect, serializedProperty, label);

					this._attributes[this._mainAttributeIndex].MainDrawInInspector(rect, serializedProperty, label);

					for (int a = 0; a < this._attributes.Count; a++)
						this._attributes[a].DrawInInspector(rect, serializedProperty, label);

					for (int a = 0; a < this._attributes.Count; a++)
						this._attributes[a].RightDrawInInspector(rect, serializedProperty, label);
				}
				EditorGUILayout.EndHorizontal();

				for (int a = 0; a < this._attributes.Count; a++)
					this._attributes[a].EndDrawInInspector(rect, serializedProperty, label);
			}
			EditorGUILayout.EndVertical();

			if (changeCheckScope.changed)
			{
				if (this._onScopeChanged == null)
					this._onScopeChanged = this.fieldInfo.DeclaringType.GetTypeInfo().GetDeclaredMethod("OnValidateMultiAttributes");
				else
					this._onScopeChanged.Invoke(serializedProperty.serializedObject.targetObject, null);
			}

			serializedProperty.serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif