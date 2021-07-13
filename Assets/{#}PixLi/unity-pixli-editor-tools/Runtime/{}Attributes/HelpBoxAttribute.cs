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
public class HelpBoxAttribute : MultiSupportPropertyAttribute
{
	public enum HelpBoxType { None, Info, Warning, Error }

	private const string INFO_BUTTON_LABEL = "?";
	private const string WARNING_BUTTON_LABEL = "!";

	private bool _showMessage;
	private string _label;

	private string _message;
	private HelpBoxType _helpBoxType;
	private bool _wide;

	public HelpBoxAttribute(string message, HelpBoxType helpBoxType = HelpBoxType.Info, bool wide = true) : base(false)
	{
		this._message = message;
		this._helpBoxType = helpBoxType;
		this._wide = wide;

		switch (this._helpBoxType)
		{
			case HelpBoxType.Warning: case HelpBoxType.Error:

				this._label = WARNING_BUTTON_LABEL;

				break;
			default:

				this._label = INFO_BUTTON_LABEL;

				break;
		}
	}

#if UNITY_EDITOR
	public override float GetPropertyHeight()
	{
		return 0f;
	}

	public override float GetPropertyExtensionWidth()
	{
		return EditorGUIUtility.singleLineHeight + 4f;
	}

	public override void MainDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	{
		EditorGUILayout.PropertyField(serializedProperty, true);
	}

	public override void RightDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	{
		//GUIStyle style = new GUIStyle(GUI.skin.box)
		//{
		//	alignment = TextAnchor.MiddleCenter,
		//	fontSize = 9,
		//};
			
		if (EditorGUILayoutUtility.ButtonMini(new GUIContent(this._label)))
			this._showMessage = !this._showMessage;
	}

	public override void EndDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label)
	{
		if (this._showMessage)
			EditorGUILayout.HelpBox(this._message, (UnityEditor.MessageType)this._helpBoxType, this._wide);
	}
#endif
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(HelpBoxAttribute))]
[CanEditMultipleObjects]
public class HelpBoxAttributeDrawer : MultiSupportPropertyAttributeDrawer
{
}
#endif