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
using Random = UnityEngine.Random;

#if UNITY_EDITOR
public class MultiSupportPropertyDrawer : PropertyDrawer
{
	protected MultiSupportAttribute[] attributes;

	public virtual void OnEnable()
	{
		this.attributes = this.fieldInfo.GetCustomAttributes(typeof(MultiSupportAttribute), true) as MultiSupportAttribute[];
	}

	public virtual void BeginDrawGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label) { }

	public virtual void LeftDrawGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label) { }
	public virtual void MainDrawGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label) => base.OnGUI(rect, serializedProperty, label);
	public virtual void DrawGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label) { }
	public virtual void RightDrawGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label) { }

	public virtual void EndDrawGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label) { }

	private MethodInfo _onScopeChanged;

	public sealed override void OnGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	{
		//using (EditorGUI.ChangeCheckScope changeCheckScope = new EditorGUI.ChangeCheckScope())
		EditorGUI.BeginChangeCheck();
		{
			//EditorGUILayout.BeginVertical();
			{
				this.BeginDrawGUI(rect, serializedProperty, label);

				this.LeftDrawGUI(rect, serializedProperty, label);
				this.MainDrawGUI(rect, serializedProperty, label);

				this.DrawGUI(rect, serializedProperty, label);

				this.RightDrawGUI(rect, serializedProperty, label);

				this.EndDrawGUI(rect, serializedProperty, label);
			}
			//EditorGUILayout.EndVertical();
		}
		EditorGUI.EndChangeCheck();
	}

	//public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
	//{
	//	return base.GetPropertyHeight(serializedProperty, label);
	//}

	//public override void OnGUI(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	//{
	//	using (EditorGUI.ChangeCheckScope changeCheckScope = new EditorGUI.ChangeCheckScope())
	//	{
	//		EditorGUILayout.BeginVertical();
	//		{
	//			this.BeginDrawGUI(rect, serializedProperty, label);

	//			this.LeftDrawGUI(rect, serializedProperty, label);
	//			this.MainDrawGUI(rect, serializedProperty, label);

	//			this.DrawGUI(rect, serializedProperty, label);

	//			this.RightDrawGUI(rect, serializedProperty, label);

	//			this.EndDrawGUI(rect, serializedProperty, label);

	//			for (int a = 0; a < this.attributes.Length; a++)
	//			{
	//				for (int b = 0; b < this.attributes.Length; b++)
	//				{
	//					//this.attributes[b].Initialize(this.target, this.serializedObject, this.members[a]);

	//					this.attributes[b].BeginDrawInInspector();

	//					EditorGUILayout.BeginHorizontal();
	//					{
	//						this.attributes[b].LeftDrawInInspector();
	//						this.attributes[b].MainDrawInInspector();
	//						this.attributes[b].DrawInInspector();
	//						this.attributes[b].RightDrawInInspector();
	//					}
	//					EditorGUILayout.EndHorizontal();

	//					this.attributes[b].EndDrawInInspector();
	//				}
	//			}
	//		}
	//		EditorGUILayout.EndVertical();

	//		if (changeCheckScope.changed)
	//		{
	//		}
	//	}
	//}
}
#endif