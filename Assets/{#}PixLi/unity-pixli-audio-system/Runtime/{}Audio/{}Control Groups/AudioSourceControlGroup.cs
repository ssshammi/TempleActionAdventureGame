using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "[Audio Source Control Group]", menuName = "[Audio]/[Audio Source Control Group]")]
public class AudioSourceControlGroup : ScriptableObject
{
	[SerializeField] private AudioSourceControlGroup _masterAudioSourceControlGroup;
	public AudioSourceControlGroup _MasterAudioSourceControlGroup => this._masterAudioSourceControlGroup;

	[SerializeField] private UnityEventFloat _onVolumeChange;
	public UnityEventFloat _OnVolumeChange => this._onVolumeChange;

	//public event Action<float> OnVolumeChange = delegate { };

	[Range(0.0f, 1.0f)]
	[SerializeField] private float _volume = 1.0f;
	public float Volume
	{
		get => this._volume * (this._masterAudioSourceControlGroup != this ? this._masterAudioSourceControlGroup.Volume : 1.0f);
		set
		{
			this._volume = Mathf.Clamp01(value: value);

			this._onVolumeChange.Invoke(arg0: this._volume);
		}
	}

#if UNITY_EDITOR
	//TODO: make a tooltip button to set it when it's missing.
	private void Awake()
	{
		string[] assetsGuids = AssetDatabase.FindAssets("t:MasterAudioSourceControlGroup", new[] { "Assets/Audio", "Assets/Audio/#Settings", "Assets/{#}Packages/unity-pixli-audio-system/#Presets/#Control Groups" });

		if (assetsGuids.Length > 0)
		{
			//this._masterAudioSourceControlGroup = AssetDatabase.LoadAssetAtPath<MasterAudioSourceControlGroup>(
			//	assetPath: AssetDatabase.GUIDToAssetPath(guid: assetsGuids[0])
			//);
		}
	}
#endif
}
