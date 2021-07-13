using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Object = UnityEngine.Object;

#if UNITY_EDITOR
//TODO: comment all of this.
/// <summary>
/// 
/// </summary>
[InitializeOnLoad]
public class Hierarchy
{
	[Serializable]
	private struct HierarchyObjectData
	{
		public int PositionIndex { get; set; }
		public int VisiblePositionIndex { get; set; }

		/// <summary>
		/// Array of parents in hierarchy up to the top.
		/// First array item is scene instanceID. All next are in order up to hierarchy.
		/// [1] == parent, [2] == parent.parent, [3] == parent.parent.parent, ...
		/// </summary>
		public int[] Ancestors { get; set; }
		public HierarchyProperty HierarchyProperty { get; set; }

		public HierarchyObjectData(int positionIndex, HierarchyProperty hierarchyProperty) : this()
		{
			this.VisiblePositionIndex = positionIndex;
			this.HierarchyProperty = hierarchyProperty;
		}
	}
		
	/// <summary>
	/// 
	/// </summary>
	/// <param name="color"></param>
	/// <param name="selectionRect"></param>
	/// <param name="style"></param>
	public static void DrawBox(Color color, Rect selectionRect, GUIStyle style)
	{
		selectionRect.size = new Vector2(Screen.width, selectionRect.size.y);

		Color temp = GUI.backgroundColor;

		GUI.backgroundColor = color;
			
		GUI.Box(selectionRect, GUIContent.none, style);

		GUI.backgroundColor = temp;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="color"></param>
	/// <param name="selectionRect"></param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void DrawBox(Color color, Rect selectionRect) => 
		Hierarchy.DrawBox(color, selectionRect, Hierarchy.S_HierarchySkin.HierarchyItemStyle);

	public static void DrawRect(Color color, Rect selectionRect)
	{
		EditorGUI.DrawRect(selectionRect, color);
	}

	public const string HIERARCHY_SKIN_EXTENTION_NAME = "Hierarchy Skin.guiskin";
	public static readonly HierarchySkin S_HierarchySkin;

	private static Dictionary<int, HierarchyObjectData> s_instanceID_hierarchyObjectData_relations;

	private static readonly Color s_hierarchyItemOverlayColor = new Color32(0, 0, 0, 12);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="instanceID"></param>
	/// <param name="selectionRect"></param>
	private static void OnHierarchyItemGUI(int instanceID, Rect selectionRect)
	{
		if (!HierarchySettings._Instance.HierarchyEnabled)
			return;

		Object @object = EditorUtility.InstanceIDToObject(instanceID);

		if (@object != null && Event.current.type == EventType.Repaint)
		{
			GameObject gameObject = @object as GameObject;
			if (gameObject != null)
			{
				//Debug.Log($"{selectionRect.y} | {selectionRect.height} | {(int)(selectionRect.y / selectionRect.height)}");
					
				Rect fullRect = new Rect(selectionRect)
				{
					xMin = 0f,
					xMax = Screen.width
				};

				if ((int)(selectionRect.y / selectionRect.height) % 2 == 0)
					Hierarchy.DrawRect(s_hierarchyItemOverlayColor, fullRect);

				// Usual
				if (s_instanceID_hierarchyObjectData_relations.TryGetValue(instanceID, out HierarchyObjectData hierarchyObjectData))
				{
					//Debug.Log(hierarchyObjectData.VisiblePositionIndex);
				}

				// Specific

				gameObject.GetComponent<IHierarchyItem>()?.OnHierarchyDraw(instanceID, selectionRect);

				//if (@object.name == DoublePointInterval.DEFAULT_BEGINNING_POINT_NAME)
				//	Hierarchy.DrawBox(new Color(0f, 1f, 1f, 0.4f), selectionRect, Hierarchy.S_HierarchySkin.HierarchyItemStyle);
				//else if (@object.name == DoublePointInterval.DEFAULT_ENDING_POINT_NAME)
				//	Hierarchy.DrawBox(new Color(1f, 0f, 0f, 0.4f), selectionRect, Hierarchy.S_HierarchySkin.HierarchyItemStyle);
			}
		}
	}

	private static void Initialize()
	{
		GameObject[] gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

		HierarchyProperty hierarchyProperty = new HierarchyProperty(HierarchyType.GameObjects);
		//SearchableEditorWindow hierarchyWindow = EditorWindow.GetWindow<SearchableEditorWindow>("SceneHierarchyWindow");
		//Debug.Log(hierarchyWindow.GetType());

		hierarchyProperty.Next(null); // Get scene. hierarchyProperty.name would return the name of the scene.

		s_instanceID_hierarchyObjectData_relations = new Dictionary<int, HierarchyObjectData>(gameObjects.Length);

		//int positionIndex = 0;
		for (int a = 0; a < gameObjects.Length; a++)
		{
			hierarchyProperty.Next(null); // Pass scene.
			//Debug.Log($"Name: {EditorUtility.InstanceIDToObject(hierarchyProperty.instanceID).name} Instance ID: {hierarchyProperty.instanceID}");
			//Debug.Log("Root: " + gameObjects[a].name);

			//Debug.Log("Row: " + hierarchyProperty.row);

			//for (int b = 0; b < hierarchyProperty.ancestors.Length; b++)
			//{
			//	Debug.Log("Anc: " + hierarchyProperty.ancestors[b]);
			//}

			//Debug.Log(hierarchyProperty.IsExpanded(new int[] { hierarchyProperty.instanceID })); // Get roots - if you are expanded - go for children and add them.

			s_instanceID_hierarchyObjectData_relations.Add(gameObjects[a].GetInstanceID(), new HierarchyObjectData(a, hierarchyProperty));

			//positionIndex++;
		}
	}

	private static void OnHierarchyChanged()
	{
		Hierarchy.Initialize();
	}

	// Some smell code I wrote to bring all objects back to the scene. Don't mess with HideFlags ☺ ♥♥♥
	private static void ResetLel(Transform transform)
	{
		HierarchyProperty hierarchyProperty = new HierarchyProperty(HierarchyType.GameObjects);
			
		hierarchyProperty.Next(null); // Get scene. hierarchyProperty.name would return the name of the scene.

		while (hierarchyProperty.Next(null))
		{
			hierarchyProperty.pptrValue.hideFlags = HideFlags.None;
		}
	}

	private static void ResetLel2<TComponent>()
	{
		HierarchyProperty hierarchyProperty = new HierarchyProperty(HierarchyType.GameObjects);

		hierarchyProperty.Next(null); // Get scene. hierarchyProperty.name would return the name of the scene.

		while (hierarchyProperty.Next(null))
		{
			hierarchyProperty.pptrValue.hideFlags = HideFlags.None;
		}
	}

	/// <summary>
	/// Assign all required editor callbacks.
	/// </summary>
	static Hierarchy()
	{
		//foreach (var item in SceneManager.GetActiveScene().GetRootGameObjects())
		//{
		//	ResetLel(item.transform);
		//}

		Hierarchy.S_HierarchySkin = AssetDatabase.LoadAssetAtPath<HierarchySkin>(
			Path.Combine(
				PathUtility.GetScriptFileDirectoryPath(),
				Hierarchy.HIERARCHY_SKIN_EXTENTION_NAME
			)
		);

		Hierarchy.Initialize();

		EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyItemGUI;
		EditorApplication.hierarchyChanged += OnHierarchyChanged;
	}
}
#endif