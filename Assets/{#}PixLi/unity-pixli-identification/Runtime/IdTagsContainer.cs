using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
//[CreateAssetMenu(fileName = "[Id Tags Container]", menuName = "[Id Tags Container]", order = 0)]
public class IdTagsContainer : ScriptableObject //: IList
{
	private const int _DEFAULT_ID_TAGS_CAPACITY = 32;

	//[DebugCollection(DebugAttribute.DebugType.Visible)]
	//[Debug]
	[SerializeField] private List<IdTag> _idTags = new List<IdTag>(_DEFAULT_ID_TAGS_CAPACITY);
	public List<IdTag> IdTags => this._idTags;

	public IdTag[] IdTagsArray => this._idTags.ToArray();

	public void Add(IdTag idTag, bool preventAdditionOfDuplicateTags = true)
	{
		if (preventAdditionOfDuplicateTags && this._idTags.Contains(idTag))
		{
			Debug.LogError("Make sure you have been trying to add duplicate tags intentionally.");

			return;
		}

		this._idTags.Add(idTag);
	}

	public int FindIndex(IdTag idTag)
	{
		for (int a = 0; a < this._idTags.Count; a++)
			if (idTag == this._idTags[a])
				return a;

		return -1;
	}

	public int FindIndex(IdTag idTag, int defaultIndex)
	{
		for (int a = 0; a < this._idTags.Count; a++)
			if (idTag == this._idTags[a])
				return a;

		return defaultIndex;
	}

	public bool Contains(IdTag idTag) => this._idTags.Contains(idTag);
	public bool Remove(IdTag idTag) => this._idTags.Remove(idTag);
	public void Clear() => this._idTags.Clear();

	public bool DrawListEO { get; set; }
	public bool SelfIndentationEO { get; set; }

	//protected virtual void OnDrawGizmos()
	//{
	//}

	[CustomEditor(typeof(IdTagsContainer)), CanEditMultipleObjects]
	public class IdTagsContainerEditor : MultiSupportEditor
	{
		private void DrawHeader(Rect rect) =>
			EditorGUI.LabelField(rect, this._idTagsProperty.displayName);

		private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
		{
			SerializedProperty element = this._idTagsProperty.GetArrayElementAtIndex(index).GetVisibleChildAt(0);

			rect.position += new Vector2(0f, 4f);

			EditorGUI.PropertyField(rect, element, GUIContent.none, true);//new GUIContent("^_^"), true);
		}

		private float GetElementHeight(int index) =>
			Mathf.Max(
				EditorGUIUtility.singleLineHeight, 
				EditorGUI.GetPropertyHeight(this._idTagsProperty.GetArrayElementAtIndex(index).GetVisibleChildAt(0), GUIContent.none, true)
			) + 8f;

		private IdTagsContainer _tIdTagsContainer;

		private SerializedProperty _idTagsProperty;
		private ReorderableList _idTagsList;

		public override void OnEnable()
		{
			base.OnEnable();
	
			this._tIdTagsContainer = this.target as IdTagsContainer;

			this._idTagsProperty = this.serializedObject.FindProperty("_idTags");

			this._idTagsList = new ReorderableList(this.serializedObject, this._idTagsProperty, true, true, true, true)
			{
				drawHeaderCallback = this.DrawHeader,
				drawElementCallback = this.DrawElement,
				elementHeightCallback = this.GetElementHeight
			};

			this._tIdTagsContainer.SelfIndentationEO = false;
		}
	
		public override void MainDrawGUI()
		{
			this.DrawDefaultInspector(false, "_idTags");

			if (this._tIdTagsContainer.DrawListEO)
			{
				if (this._tIdTagsContainer.SelfIndentationEO)
					EditorGUI.indentLevel -= 3;

				this._idTagsList.DoLayoutList();

				if (this._tIdTagsContainer.SelfIndentationEO)
					EditorGUI.indentLevel += 3;
			}
			else
				DebugAttribute.PropertyField(this._idTagsProperty, true, DebugAttribute.DebugType.Visible, 2);
				//EditorGUILayout.PropertyField(this._idTagsProperty, true);
		}
	}
}
#endif