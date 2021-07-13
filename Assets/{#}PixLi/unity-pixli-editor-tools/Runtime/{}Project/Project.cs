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
[InitializeOnLoad]
public class Project
{
	private static Texture s_padlockTexture;

	private static readonly Color s_projectItemOverlayColor = new Color32(0, 0, 0, 12);

	//private static readonly Color s_projectItemDefaultHoverColor = EditorGUIUtility.isProSkin ? new Color32(255, 255, 255, 44) : new Color32(0, 0, 0, 44);
	private static readonly Color s_projectItemOverlayHoverColor = EditorGUIUtility.isProSkin ? new Color32(255, 255, 255, 32) : new Color32(0, 0, 0, 32);

	private static void OnProjectItemGUI(string guid, Rect selectionRect)
	{
		if (!HierarchySettings._Instance.HierarchyEnabled)
			return;

		Rect fullRect = new Rect(selectionRect)
		{
			xMin = 0f,
			xMax = Screen.width
		};

		Object @object = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(guid));

		if (@object != null)
		{
			if (fullRect.Contains(Event.current.mousePosition) 
				&& Selection.activeObject != @object)
			{
				Hierarchy.DrawRect(s_projectItemOverlayHoverColor, fullRect);
			}

			switch (Event.current.type)
			{
				//case EventType.MouseDown:
				//	break;
				//case EventType.MouseUp:
				//	break;
				//case EventType.MouseMove:

				//	EditorApplication.RepaintProjectWindow();

				//	break;
				//case EventType.MouseDrag:
				//	break;
				//case EventType.KeyDown:
				//	break;
				//case EventType.KeyUp:
				//	break;
				//case EventType.ScrollWheel:
				//	break;
				case EventType.Repaint:

					if ((int)(selectionRect.y / selectionRect.height) % 2 == 0
						&& !fullRect.Contains(Event.current.mousePosition)
						&& Selection.activeObject != @object)
					{
						Hierarchy.DrawRect(s_projectItemOverlayColor, fullRect);
					}

					if (EditorAssetModificationProcessor.S_LockedFilePaths.Contains(@object.GetInstanceID()))
					{
						float iconSize = EditorGUIUtility.singleLineHeight / 2f;
						Rect padlockIconRect = new Rect(fullRect)
						{
							width = iconSize,
							height = iconSize,
						};
						padlockIconRect.position += new Vector2(4f, (EditorGUIUtility.singleLineHeight - iconSize) / 2f - 0.1f);

						GUI.DrawTexture(padlockIconRect, s_padlockTexture);
					}

					break;
					//case EventType.Layout:
					//	break;
					//case EventType.DragUpdated:
					//	break;
					//case EventType.DragPerform:
					//	break;
					//case EventType.DragExited:
					//	break;
					//case EventType.Ignore:
					//	break;
					//case EventType.Used:
					//	break;
					//case EventType.ValidateCommand:
					//	break;
					//case EventType.ExecuteCommand:
					//	break;
					//case EventType.ContextClick:
					//	break;
					//case EventType.MouseEnterWindow:
					//	break;
					//case EventType.MouseLeaveWindow:
					//	break;
			}
		}
	}

	private static void OnProjectChanged()
	{
	}

	private static void Initialize()
	{
		s_padlockTexture = Resources.Load<Texture>("padlock 16");
	}

	//TODO: move this to separate utility.
	private static void OnEditorUpdate()
	{
		EditorWindow currentWindow = EditorWindow.mouseOverWindow;
		if (currentWindow != null && currentWindow.GetType() == s_projectWindowType)
		{
			//if (!currentWindow.wantsMouseMove)
			//{
			//	//allow the hierarchy window to use mouse move events!
			//	currentWindow.wantsMouseMove = true;
			//}

			//Debug.Log(Event.current?.type);

			//if (Event.current?.type == EventType.MouseMove)
			EditorApplication.RepaintProjectWindow();
		}
	}

	private static readonly Type s_projectWindowType;

	static Project()
	{
		Project.Initialize();

		EditorApplication.projectWindowItemOnGUI += OnProjectItemGUI;
		EditorApplication.projectChanged += OnProjectChanged;

		// lel

		EditorApplication.update += OnEditorUpdate;

		Assembly editorAssembly = typeof(EditorWindow).Assembly;
		s_projectWindowType = editorAssembly.GetType("UnityEditor.ProjectBrowser");
	}
}
#endif