using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
public class MultiSupportEditor : Editor
{
	protected MemberInfo[] members;

	public virtual void OnEnable()
	{
		this.members = this.target.GetType().GetMembers(
			BindingFlags.Public |
			BindingFlags.NonPublic |
			BindingFlags.Static |
			BindingFlags.Instance
		);
	}

	public virtual void BeginDrawGUI() { }

	public virtual void LeftDrawGUI() { }
	public virtual void MainDrawGUI() => base.OnInspectorGUI();
	public virtual void DrawGUI() { }
	public virtual void RightDrawGUI() { }

	public virtual void EndDrawGUI() { }

	private MethodInfo _onScopeChanged;

	public sealed override void OnInspectorGUI()
	{
		this.serializedObject.Update();

		//using (EditorGUI.ChangeCheckScope changeCheckScope = new EditorGUI.ChangeCheckScope())
		EditorGUI.BeginChangeCheck();
		{
			EditorGUILayout.BeginVertical();
			{
				this.BeginDrawGUI();

				this.LeftDrawGUI();
				this.MainDrawGUI();

				this.DrawGUI();

				this.RightDrawGUI();

				this.EndDrawGUI();

				for (int a = 0; a < this.members.Length; a++)
				{
					MultiSupportAttribute[] multiSupportAttributes = this.members[a].GetCustomAttributes<MultiSupportAttribute>(true) as MultiSupportAttribute[];

					for (int b = 0; b < multiSupportAttributes.Length; b++)
					{
						multiSupportAttributes[b].Initialize(this.target, this.serializedObject, this.members[a]);

						multiSupportAttributes[b].BeginDrawInInspector();

						EditorGUILayout.BeginHorizontal();
						{
							multiSupportAttributes[b].LeftDrawInInspector();
							multiSupportAttributes[b].MainDrawInInspector();
							multiSupportAttributes[b].DrawInInspector();
							multiSupportAttributes[b].RightDrawInInspector();
						}
						EditorGUILayout.EndHorizontal();

						multiSupportAttributes[b].EndDrawInInspector();
					}
				}
			}
			EditorGUILayout.EndVertical();

			if (EditorGUI.EndChangeCheck())//changeCheckScope.changed)
			{
				if (this._onScopeChanged == null)
					this._onScopeChanged = this.target.GetType().GetTypeInfo().GetDeclaredMethod("OnScopeChanged");
				else
					this._onScopeChanged.Invoke(this.target, null);
			}
		}

		this.serializedObject.ApplyModifiedProperties();
	}

	public static void DrawDefaultInspector(SerializedProperty serializedProperty, params string[] avoidFieldNames)
	{
		HashSet<string> avoidanceHashSet = new HashSet<string>(avoidFieldNames);

		bool hasNextVisible = serializedProperty.NextVisible(true);
		while (hasNextVisible)
		{
			if (!avoidanceHashSet.Contains(serializedProperty.name))
				EditorGUILayout.PropertyField(serializedProperty, true);

			hasNextVisible = serializedProperty.NextVisible(false);
		}

		serializedProperty.serializedObject.ApplyModifiedProperties();
	}

	public static void DrawDefaultInspectorChildren(SerializedProperty serializedProperty, params string[] avoidFieldNames)
	{
		int initialDepth = serializedProperty.depth;
			
		HashSet<string> avoidanceHashSet = new HashSet<string>(avoidFieldNames);

		bool hasNextVisible = serializedProperty.NextVisible(true);
		while (hasNextVisible && serializedProperty.depth > initialDepth)
		{
			if (!avoidanceHashSet.Contains(serializedProperty.name))
				EditorGUILayout.PropertyField(serializedProperty, true);

			hasNextVisible = serializedProperty.NextVisible(false);
		}

		serializedProperty.serializedObject.ApplyModifiedProperties();
	}

	protected virtual void DrawDefaultInspector(bool drawScriptField = true, params string[] avoidFieldNames)
    {
        SerializedProperty serializedProperty = this.serializedObject.GetIterator();
        serializedProperty.NextVisible(true); // Ommit (Base) dropdown.

        if (drawScriptField)
        {
            EditorGUI.BeginDisabledGroup(true);
            {
            EditorGUILayout.PropertyField(serializedProperty, true); // Script field.
            }
            EditorGUI.EndDisabledGroup();
        }

        MultiSupportEditor.DrawDefaultInspector(serializedProperty, avoidFieldNames);
    }
        
    protected virtual void DrawDefaultInspector(params string[] avoidFieldNames)
    {
        this.DrawDefaultInspector(true, avoidFieldNames);
    }
}

[CustomEditor(typeof(MonoBehaviour), true)]
[CanEditMultipleObjects]
public class MonoBehaviourEditor : MultiSupportEditor
{
}

[CustomEditor(typeof(ScriptableObject), true)]
[CanEditMultipleObjects]
public class ScriptableObjectEditor : MultiSupportEditor
{
}
#endif