using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class ExposeAttribute : MultiSupportPropertyAttribute
{
	public enum ExposeMode { Custom, Native }

	private readonly ExposeMode _exposeMode;

	public ExposeAttribute(ExposeMode exposeMode = ExposeMode.Custom) : base(true)
	{
		this._exposeMode = exposeMode;
	}

	public ExposeAttribute(ExposeMode exposeMode, bool drawMain) : base(drawMain)
	{
		this._exposeMode = exposeMode;
	}

#if UNITY_EDITOR
	public override float GetPropertyHeight()
	{
		return this._exposeMode == ExposeMode.Custom ? 0f : base.GetPropertyHeight();
	}
		
	private Editor _editor;

	public override void MainDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	{
		if (this.serializedProperty.propertyType != SerializedPropertyType.ObjectReference)
		{
			base.MainDrawInInspector(rect, serializedProperty, label);

			return;
		}

		switch (this._exposeMode)
		{
			case ExposeMode.Custom:

				EditorGUILayout.BeginVertical();
				{
					Object @object = serializedProperty.objectReferenceValue;

					if (@object != null)
					{
						Rect horizontal = EditorGUILayout.BeginHorizontal();
						{
							horizontal.position += new Vector2(-4f, 0f);
							horizontal.width = EditorGUIUtility.labelWidth;

							serializedProperty.isExpanded = EditorGUI.Foldout(horizontal, serializedProperty.isExpanded, GUIContent.none, true);

							EditorGUILayout.PropertyField(
								serializedProperty,
								new GUIContent(string.IsNullOrEmpty(@object.name) ? serializedProperty.displayName : @object.name),
								true
							);
						}
						EditorGUILayout.EndHorizontal();

						if (serializedProperty.isExpanded)
						{
							Editor.CreateCachedEditor(@object, null, ref this._editor);

							EditorGUI.indentLevel++;
							{
								this._editor.OnInspectorGUI();
							}
							EditorGUI.indentLevel--;

							GUILayout.Space(2f);
						}
					}
					else
					{
						//Rect horizontal = EditorGUILayout.BeginHorizontal();
						//{
							EditorGUILayout.PropertyField(serializedProperty, true);
						//}
						//EditorGUILayout.EndHorizontal();
					}
				}
				EditorGUILayout.EndVertical();

				break;
			case ExposeMode.Native:

				EditorGUILayout.BeginVertical();
				{
					base.MainDrawInInspector(rect, serializedProperty, label);

					Object @object = serializedProperty.objectReferenceValue;

					if (@object != null)
					{
						serializedProperty.isExpanded = EditorGUILayout.InspectorTitlebar(serializedProperty.isExpanded, @object);

						if (serializedProperty.isExpanded)
						{
							Editor.CreateCachedEditor(@object, null, ref this._editor);

							this._editor.OnInspectorGUI();
						}

						EditorGUILayoutUtility.HorizontalLine();
					}
				}
				EditorGUILayout.EndVertical();

				break;
		}
	}
#endif
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ExposeAttribute))]
[CanEditMultipleObjects]
public class ExposeAttributeDrawer : MultiSupportPropertyAttributeDrawer
{
}
#endif