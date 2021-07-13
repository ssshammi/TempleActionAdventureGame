using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
using Unity.EditorCoroutines.Editor;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
[CreateAssetMenu(fileName = "[Id Tag System]", menuName = "[Id Tag System]/[Id Tag System]")]
public class IdTagSystem : ScriptableObjectSingleton<IdTagSystem>
{
	[Serializable]
	private struct Combination
	{
		public IdTagsContainer IdTagsContainer;
		public IdTag IdTag;

		public bool Unique;

		public static bool Contains(List<Combination> combinations, IdTag idTag, out int index)
		{
			for (int a = 0; a < combinations.Count; a++)
			{
				if (combinations[a].IdTag == idTag)
				{
					index = a;

					return true;
				}
			}

			index = -1;

			return false;
		}

		public Combination(IdTagsContainer idTagsContainer, IdTag idTag, bool unique = true)
		{
			this.IdTagsContainer = idTagsContainer;
			this.IdTag = idTag;

			this.Unique = unique;
		}
	}

	//[Debug(DebugAttribute.DebugType.Visible)]
	[Expose]
	[SerializeField] private List<IdTagsContainer> _idTagsContainers = new List<IdTagsContainer>();
	public IdTagsContainer[] IdTagsContainers => this._idTagsContainers.ToArray();

	/// <summary>
	/// (DO NOT USE THIS METHOD FOR YOUR OWN GOOD)
	/// </summary>
	/// <param name="name">(DO NOT USE THIS METHOD FOR YOUR OWN GOOD) It's type of class created from container that is located in IdTagStaticAccess.cs file.</param>
	/// <returns>(DO NOT USE THIS METHOD FOR YOUR OWN GOOD)</returns>
	public IdTagsContainer GetIdTagsContainer(Type type) => this._idTagsContainers.Find((idTagsContainer) => idTagsContainer.name.ToValidClassName() == type.Name);

	[HelpBox("A virtual container that holds all IdTags. Unique/(No shared/repeatable) tags. No path/prefixes.")]
	[Expose]
	[SerializeField] private IdTagsContainer _allIdTagsContainer;
	public IdTagsContainer AllIdTagsContainer => this._allIdTagsContainer;

	[SerializeField] private string[] _allIdTagsRelativePaths;
	public string[] AllIdTagsRelativePaths => this._allIdTagsRelativePaths;

	[HelpBox("A virtual container that holds all shared(the ones that repeat) IdTags.")]
	[Expose]
	[SerializeField] private IdTagsContainer _sharedIdTagsContainer;
	public IdTagsContainer SharedIdTagsContainer => this._sharedIdTagsContainer;

	[HelpBox("A virtual container that holds all unique(the ones that don't repeat) IdTags.")]
	[Expose]
	[SerializeField] private IdTagsContainer _uniqueIdTagsContainer;
	public IdTagsContainer UniqueIdTagsContainer => this._uniqueIdTagsContainer;

	[HelpBox("A virtual container that holds a complete collection of IdTags without repeatable ids.")]
	[Expose]
	[SerializeField] private IdTagsContainer _mixedIdTagsContainer;
	public IdTagsContainer MixedIdTagsContainer => this._mixedIdTagsContainer;

	[SerializeField] private string[] _mixedIdTagsRelativePaths;
	public string[] MixedIdTagsRelativePaths => this._mixedIdTagsRelativePaths;

	[SerializeField] private List<Combination> _combinations;

	internal void Initialize()
	{
		// All.
		int allIdTagsEntitiesQuantity = this._idTagsContainers.Sum((el) => el.IdTags.Count) + 1;

		if (this._allIdTagsContainer == null)
			this._allIdTagsContainer = ScriptableObject.CreateInstance<IdTagsContainer>();
		this._allIdTagsContainer.Clear();

		this._allIdTagsContainer.Add(IdTag.Default);

		this._allIdTagsRelativePaths = new string[allIdTagsEntitiesQuantity];

		int allIdTagsIndex = 0;
		this._allIdTagsRelativePaths[allIdTagsIndex++] = IdTag.Default.Tag;

		// Shared.
		if (this._sharedIdTagsContainer == null)
			this._sharedIdTagsContainer = ScriptableObject.CreateInstance<IdTagsContainer>();
		this._sharedIdTagsContainer.Clear();

		// Unique.
		if (this._uniqueIdTagsContainer == null)
			this._uniqueIdTagsContainer = ScriptableObject.CreateInstance<IdTagsContainer>();
		this._uniqueIdTagsContainer.Clear();

		// Combinations.
		this._combinations = new List<Combination>(allIdTagsEntitiesQuantity)
		{
			new Combination(this._sharedIdTagsContainer, IdTag.Default, false)
		};

		//EditorAssetModificationProcessor.S_LockedFilePaths.Add(this.GetInstanceID());

		for (int a = 0; a < this._idTagsContainers.Count; a++)
		{
			IdTag[] idTags = this._idTagsContainers[a].IdTagsArray;

			for (int b = 0; b < idTags.Length; b++)
			{
				if (Combination.Contains(this._combinations, idTags[b], out int combinationIndex) &&
					this._combinations[combinationIndex].Unique)
				{
					Combination combination = this._combinations[combinationIndex];

					combination.IdTagsContainer = this._sharedIdTagsContainer;
					combination.Unique = false;

					this._combinations[combinationIndex] = combination;
				}
				else
					this._combinations.Add(new Combination(this._idTagsContainers[a], idTags[b], true));

				//! Fill all unconditionally.
				this._allIdTagsContainer.Add(idTags[b], false);
				this._allIdTagsRelativePaths[allIdTagsIndex++] = $"{this._idTagsContainers[a].name}/{idTags[b].Tag}";
			}

			this._idTagsContainers[a].DrawListEO = true; // Allow to draw reorderable list.
		}

		if (this._mixedIdTagsContainer == null)
			this._mixedIdTagsContainer = ScriptableObject.CreateInstance<IdTagsContainer>();

		this._mixedIdTagsContainer.Clear();

		this._mixedIdTagsRelativePaths = new string[this._combinations.Count];
		for (int a = 0; a < this._combinations.Count; a++)
		{
			if (this._combinations[a].Unique)
			{
				this._uniqueIdTagsContainer.Add(this._combinations[a].IdTag);

				this._mixedIdTagsRelativePaths[a] = $"{this._combinations[a].IdTagsContainer.name}/{this._combinations[a].IdTag.Tag}";
			}
			else
			{
				this._sharedIdTagsContainer.Add(this._combinations[a].IdTag);

				this._mixedIdTagsRelativePaths[a] = this._combinations[a].IdTag.Tag;
			}

			this._mixedIdTagsContainer.Add(this._combinations[a].IdTag);
		}
	}

	private void AddIdTagsContainer(string name)
	{
		for (int a = 0; a < this._idTagsContainers.Count; a++)
		{
			if (this._idTagsContainers[a].name == name)
			{
				Debug.Log("Container name has already been taken.");
				return;
			}
		}
		this._idTagsContainers.Add(this.CreateAssetAsChild<IdTagsContainer>(name));

		//TODO: What is this dirty hack?
		EditorCoroutineUtility.StartCoroutine(
			routine: CoroutineProcessorsCollection.InvokeAfter(0.5f, this.Initialize),
			owner: this
		);
	}

	private void RemoveIdTagsContainer(Object @object)
	{
		IdTagsContainer idTagsContainer = (IdTagsContainer)@object;

		this._idTagsContainers.Remove(idTagsContainer);

		this.RemoveChildAsset(idTagsContainer);

		this.Update();
	}

	private void AddAssetsAsChildren()
	{
		if (this._allIdTagsContainer != null && this.GetChildAsset<IdTagsContainer>(@"{All Id Tags Container}") == null)
			this.AddAssetAsChild(this._allIdTagsContainer, @"{All Id Tags Container}");

		if (this._sharedIdTagsContainer != null && this.GetChildAsset<IdTagsContainer>(@"{Shared Id Tags Container}") == null)
			this.AddAssetAsChild(this._sharedIdTagsContainer, @"{Shared Id Tags Container}");

		if (this._uniqueIdTagsContainer != null && this.GetChildAsset<IdTagsContainer>(@"{Unique Id Tags Container}") == null)
			this.AddAssetAsChild(this._uniqueIdTagsContainer, @"{Unique Id Tags Container}");

		if (this._mixedIdTagsContainer != null && this.GetChildAsset<IdTagsContainer>(@"{Mixed Id Tags Container}") == null)
			this.AddAssetAsChild(this._mixedIdTagsContainer, @"{Mixed Id Tags Container}");
	}

	//private void AddGeneratedIdTagStaticAccess(this StringBuilder stringBuilder, string tag)
	//{
	//	tag = tag.Replace(" ", string.Empty);

	//	if (string.IsNullOrEmpty(tag))
	//		return;
	//}

	//[Button(30)]
	private void GenerateIdTagsStaticAccess()
	{
		//DirectoryInfo directoryInfo = Directory.CreateDirectory(Path.Combine(Application.dataPath, "_Specific"));

		using (StreamWriter streamWriter = new StreamWriter(Path.Combine(PathUtility.GetScriptFileDirectoryPath(), "IdTagStaticAccess.cs")))
		{
			string formatBase = $@"/* Created by Robots */

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

public partial struct IdTag
{{{{
{{0}}}}}}";

			StringBuilder generatedStaticAccess = new StringBuilder(this._idTagsContainers.Count);

			string containerStaticFormat = $@"	public static class {{0}}
	{{{{
{{1}}	}}}}";

			for (int a = 0; a < this._idTagsContainers.Count; a++)
			{
				IdTagsContainer idTagsContainer = this._idTagsContainers[a];

				StringBuilder staticEntities = new StringBuilder(idTagsContainer.IdTags.Count);

				for (int b = 0; b < idTagsContainer.IdTags.Count; b++)
				{
					staticEntities.AppendLine(
						$@"		public static IdTag {idTagsContainer.IdTags[b].Tag.ToValidClassName()} = new IdTag(""{idTagsContainer.IdTags[b].Tag.ToValidClassName()}"");"
					);
				}

				generatedStaticAccess.AppendLine(
					string.Format(
						containerStaticFormat,
						idTagsContainer.name.ToValidClassName(),
						staticEntities
					)
				);
			}
			
			streamWriter.Write(
				string.Format(
					formatBase,
					generatedStaticAccess
				)
			);
		}

		EditorUtility.SetDirty(this);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	[Button(50)]
	public void Update()
	{
		for (int a = 0; a < this._idTagsContainers.Count; a++)
		{
			IdTagsContainer idTagsContainer = this._idTagsContainers[a];

			for (int b = 0; b < idTagsContainer.IdTags.Count; b++)
			{
				idTagsContainer.IdTags[b] = idTagsContainer.IdTags[b].ResetEO();
			}
		}

		this.Initialize();

		this.GenerateIdTagsStaticAccess();

		this.AddAssetsAsChildren();
	}

	[Button(40)]
	public bool Validate()
	{
		for (int a = this._allIdTagsContainer.IdTags.Count - 1; a >= 0; a--)
		{
			for (int b = 0; b < a; b++)
			{
				if (this._allIdTagsContainer.IdTags[a] == this._allIdTagsContainer.IdTags[b] &&
					this._allIdTagsContainer.IdTags[a].Tag != this._allIdTagsContainer.IdTags[b].Tag)
				{
					Debug.Log($@"<color=""red"">Invalid. Found similar Ids with different Tags. {this._allIdTagsContainer.IdTags[a]} & {this._allIdTagsContainer.IdTags[b]}</color>");

					return false;
				}
			}
		}

		Debug.Log($@"<color=""lime"">Valid. You are good to proceed!</color>");

		return true;
	}

	//private IEnumerator ResetProcess()
	//{
	//	this.Initialize();

	//	yield return new WaitForSeconds(0.5f);

	//	this.AddAssetsAsChildren();
	//}

	//public void Reset()
	//{
	//	EditorCoroutineUtility.StartCoroutine(
	//		routine: this.ResetProcess(),
	//		owner: this
	//	);
	//}

	//private void Awake()
	//{
	//	this.Reset();
	//}

	protected override string GetInstanceDirectoryPath() => PathUtility.GetRelativePath(Directory.CreateDirectory(Path.Combine(Application.dataPath, "_Specific")).FullName);

	//protected virtual void OnDrawGizmos()
	//{
	//}

	[HelpBox("When selected - it will combine identical tags from different containers in IdTag dropdown and it will appear without preceeding path, just a tag.")]
	[SerializeField] private bool _combineIdenticalIdTagsInSelection = false;
	public bool CombineIdenticalIdTagsInSelection => this._combineIdenticalIdTagsInSelection;

	[CustomEditor(typeof(IdTagSystem)), CanEditMultipleObjects]
	public class IdTagSystemEditor : MultiSupportEditor
	{
		private IdTagSystem _tIdTagSystem;

		private SerializedProperty _idTagsContainersProperty;

		public override void OnEnable()
		{
			base.OnEnable();

			this._tIdTagSystem = this.target as IdTagSystem;

			this._idTagsContainersProperty = this.serializedObject.FindProperty("_idTagsContainers");
		}

		public static GUIStyle GetBoxStyle(Texture2D texture2D, RectOffset padding)
		{
			GUIStyle style = new GUIStyle(GUI.skin.box);
			style.normal.background = texture2D;
			style.padding = padding;

			return style;
		}

		public static Texture2D CreateTexture(int width, int height, Color[] pixels)
		{
			Texture2D texture = new Texture2D(width, height);
			texture.SetPixels(pixels);
			texture.Apply();

			return texture;
		}

		public static Texture2D CreateTexture(int width, int height, Color color, Color borderColor, Texture2DBorder texture2DBorder, float stylizationFactor = 1f)
		{
			stylizationFactor = Mathf.Clamp01(stylizationFactor);

			Color[] pixels = new Color[width * height];

			// Full
			for (int i = 0; i < pixels.Length; ++i)
				pixels[i] = color;

			// Left
			for (int y = 0; y < (int)(width * height * Mathf.Clamp01((stylizationFactor + 0.2f))); y += width)
				for (int x = 0; x < texture2DBorder.LeftWidth; x++)
					pixels[y + x] = borderColor;

			// Right
			for (int y = width - 1; y < width * height; y += width)
				for (int x = 0; x < texture2DBorder.RightWidth; x++)
					pixels[y - x] = borderColor;

			// Top
			for (int x = width * height - 1; x > width * height - 1 - (int)(width * stylizationFactor); x--)
				for (int y = 0; y < texture2DBorder.TopWidth; y++)
					pixels[x - y * width] = borderColor;

			// Bottom
			for (int x = 0; x < width; x++)
				for (int y = 0; y < texture2DBorder.BottomWidth; y++)
					pixels[x + y * width] = borderColor;

			return CreateTexture(width, height, pixels);
		}

		public struct Texture2DBorder
		{
			public int LeftWidth;
			public int RightWidth;
			public int TopWidth;
			public int BottomWidth;

			public Texture2DBorder(int leftWidth = 1, int rightWidth = 1, int topWidth = 1, int bottomWidth = 1)
			{
				this.LeftWidth = leftWidth;
				this.RightWidth = rightWidth;
				this.TopWidth = topWidth;
				this.BottomWidth = bottomWidth;
			}
		}

		private bool _additionMode;

		private string _futureIdTagsContainerName;

		public override void MainDrawGUI()
		{
			GUIStyle style = GetBoxStyle(
				CreateTexture(
					10,
					50,
					UnityEditorInternal.InternalEditorUtility.HasPro() ? new Color(0f, 0f, 0f, 0.1f) : new Color(0.8f, 0.8f, 0.8f, 0.5f),
					new Color(0f, 0f, 0f, 0.3f),
					new Texture2DBorder(1, 1, 1, 1)
				),
				new RectOffset(5, 5, 0, 0)
			);

			++EditorGUI.indentLevel;
			Rect verticalMainRect = EditorGUILayout.BeginVertical(style);
			//verticalMainRect.position += new Vector2(-10f, 0f);
			{   //! _idTagsContainers.

				this._idTagsContainersProperty.isExpanded = EditorGUILayout.Foldout(
					this._idTagsContainersProperty.isExpanded,
					"[Main] (IdTag)s Containers",
					true
				);

				if (this._idTagsContainersProperty.isExpanded)
				{
					// SerializedProperty childPropertyIdTags = this._idTagsContainersProperty.FindProperty("_idTags");

					EditorGUILayout.BeginVertical();
					using (EditorGUI.IndentLevelScope indentLevelScope = new EditorGUI.IndentLevelScope())
					{
						EditorGUI.indentLevel -= 2;
						EditorGUILayout.HelpBox("A collection of all separate containers containing (IdTag)s.", MessageType.None, true);
						EditorGUI.indentLevel += 2;

						for (int a = 0; a < this._idTagsContainersProperty.arraySize; a++)
						{
							//Rect horizontal = EditorGUILayout.BeginHorizontal();
							{
								//Rect buttonRect = new Rect(horizontal);

								EditorGUILayout.BeginHorizontal();

								GUILayout.FlexibleSpace();

								this._tIdTagSystem._idTagsContainers[a].SelfIndentationEO = true;

								if (GUILayout.Button("X", GUILayout.Width(20f), GUILayout.Height(20f)))
								{
									this._tIdTagSystem.RemoveIdTagsContainer(this._idTagsContainersProperty.GetArrayElementAtIndex(a).objectReferenceValue);
								}

								EditorGUILayout.EndHorizontal();

								EditorGUILayout.PropertyField(this._idTagsContainersProperty.GetArrayElementAtIndex(a), true);
							}
							//EditorGUILayout.EndHorizontal();
						}

						EditorGUILayout.Space();

						if (this._additionMode)
						{
							EditorGUI.indentLevel -= 2;

							this._futureIdTagsContainerName = EditorGUILayout.TextField("New Id Tags Container Name", this._futureIdTagsContainerName);

							if (GUILayout.Button("Add"))
							{
								if (!string.IsNullOrEmpty(this._futureIdTagsContainerName))
								{
									try
									{
										this._tIdTagSystem.AddIdTagsContainer(this._futureIdTagsContainerName);
									}
									catch (Exception)
									{
										Debug.LogError("Couldn't add container, make sure you are entering a valid name.");
									}

									this._futureIdTagsContainerName = "";
								}

								this._additionMode = false;
							}

							EditorGUI.indentLevel += 2;
						}
						else
						{
							EditorGUILayout.BeginHorizontal();
							{
								GUILayout.FlexibleSpace();

								if (GUILayout.Button("+", GUILayout.Width(58f)))
								{
									this._additionMode = true;
								}

								GUILayout.Space(7);
							}
							EditorGUILayout.EndHorizontal();

							EditorGUILayout.Space();
						}
					}
					EditorGUILayout.EndVertical();
				}
			}
			EditorGUILayout.EndVertical();

			this.DrawDefaultInspector(
				false,
				// Main.
				"_idTagsContainers",

				// Containers.
				"_allIdTagsContainer",
				"_sharedIdTagsContainer",
				"_uniqueIdTagsContainer",
				"_mixedIdTagsContainer",

				// Strings.
				"_allIdTagsRelativePaths",
				"_mixedIdTagsRelativePaths",

				// Combinations.
				"_combinations"
			);

			bool enabled = GUI.enabled;
			GUI.enabled = false;

			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("_allIdTagsContainer"), true);
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("_sharedIdTagsContainer"), true);
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("_uniqueIdTagsContainer"), true);
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("_mixedIdTagsContainer"), true);

			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("_allIdTagsRelativePaths"), true);
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("_mixedIdTagsRelativePaths"), true);

			GUI.enabled = enabled;
		}
	}
}
#endif