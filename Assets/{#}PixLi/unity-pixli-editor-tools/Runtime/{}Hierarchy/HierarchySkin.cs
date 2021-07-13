using System;
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
[ExecuteInEditMode]
public class HierarchySkin : ScriptableObject
{
	[SerializeField] private GUIStyle hierarchyItemStyle;
	public GUIStyle HierarchyItemStyle => this.hierarchyItemStyle;

	[SerializeField] private GUISettings settings;
	public GUISettings Settings => this.settings;

	[MenuItem("Assets/Create/Hierarchy Skin", priority = 120)]
	public static void Create()
	{
		ScriptableObjectUtility.CreateAsset<HierarchySkin>(".guiskin");
	}
}
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(HierarchySkin))]
[CanEditMultipleObjects]
public class HierarchySkinEditor : MultiSupportEditor
{
#pragma warning disable 0219, 414
	private HierarchySkin _sHierarchySkin;
#pragma warning restore 0219, 414

	public override void OnEnable()
	{
		base.OnEnable();

		this._sHierarchySkin = this.target as HierarchySkin;
	}

	public override void MainDrawGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif