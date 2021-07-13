using System;
using System.Linq;
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

[Serializable]
public partial struct IdTag : IEquatable<IdTag>
{
	public const string DEFAULT_TAG = "Default";

	public static IdTag Default = new IdTag(IdTag.DEFAULT_TAG);

	[SerializeField] private string _tag;
	public string Tag => this._tag;

	[SerializeField] private int _id;
	public int Id => this._id;

	public IdTag(string tag)
	{
		this._tag = tag;
		
		this._id = this._tag.GetHashCode();//TODO: wrong, it's better not to use this method as id. Hash collision is a common thing, int is not an exception.
	}

	public override string ToString() => $"Tag: {this._tag.ToDisplayValue()} | Tag Id: {this._id}";

	public static bool operator ==(IdTag left, IdTag right) => left.Id == right.Id;
	public static bool operator !=(IdTag left, IdTag right) => left.Id != right.Id;

	public override int GetHashCode() => this._id;

	public override bool Equals(object obj) => base.Equals(obj);

	public bool Equals(IdTag other) => this == other;

#if UNITY_EDITOR
	internal IdTag ResetEO()
	{
		this._id = this._tag.GetHashCode();

		return this;
	}

	[CustomPropertyDrawer(typeof(IdTag)), CanEditMultipleObjects]
	public class IdTagPropertyDrawer : MultiSupportPropertyDrawer
	{
		//public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 0;

		private int _previouslySelectedIndex;

		private SerializedProperty _originalProperty, _tagProperty, _idProperty;

		public override void MainDrawGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
		{
			//? No caching because some weird magic is happening behind the scenes.
			// Unity just decides that it's ok to use this property drawer instance for all instances of IdTag in a list.
			// Thus caching happens only once, which leads to displaying old cached SerializedProperties not belonging to current instance.
			this._originalProperty = serializedProperty.Copy();

			serializedProperty.Next(true);
			this._tagProperty = serializedProperty.Copy();

			serializedProperty.Next(false);
			this._idProperty = serializedProperty.Copy();

			this._previouslySelectedIndex = -1;

			//! 

			IdTagsContainer idTagsContainer;
			string[] idTagsRelativePaths;

			if (IdTagSystem._Instance.CombineIdenticalIdTagsInSelection)
			{
				idTagsContainer = IdTagSystem._Instance.MixedIdTagsContainer;
				idTagsRelativePaths = IdTagSystem._Instance.MixedIdTagsRelativePaths;
			}
			else
			{
				idTagsContainer = IdTagSystem._Instance.AllIdTagsContainer;
				idTagsRelativePaths = IdTagSystem._Instance.AllIdTagsRelativePaths;
			}

			IdTag idTag = new IdTag(this._tagProperty.stringValue);

			//Debug.Log("Tag string value: " + this._tagProperty.stringValue);
			//Debug.Log("Serizlied Property DspName: " + serializedProperty.displayName);

			int selectedIndex = EditorGUI.Popup(
				position,
				this._originalProperty.displayName,
				idTagsContainer.FindIndex(idTag, 0),
				idTagsRelativePaths
			);

			if (selectedIndex != this._previouslySelectedIndex)
			{
				if (selectedIndex >= idTagsRelativePaths.Length)
				{
					EditorGUILayout.HelpBox("Selected tag index is out of bounds. Try to regenerate tags, some of them might be missing or serialization could have gone wrong.", MessageType.Error);
					return;
				}

				this._tagProperty.stringValue = idTagsContainer.IdTags[selectedIndex]._tag;
				this._idProperty.intValue = idTagsContainer.IdTags[selectedIndex]._id;

				this._previouslySelectedIndex = selectedIndex;
			}
		}
	}
#endif
}