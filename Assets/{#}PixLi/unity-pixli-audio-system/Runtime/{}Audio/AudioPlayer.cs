using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AudioPlayer : MonoBehaviourSingleton<AudioPlayer>
{
	//TODO: Remove ability to change size of this array.
	[SerializeField] private AudioPlayer.Data[] _audioPlayerData = new AudioPlayer.Data[typeof(IdTag.Audio).GetFields(BindingFlags.Public | BindingFlags.Static).Length];
	public AudioPlayer.Data[] _AudioPlayerData => this._audioPlayerData;

	private Dictionary<IdTag, AudioSourcePlayer> _audioSourcePlayers = new Dictionary<IdTag, AudioSourcePlayer>();

	public AudioSourceController Play(AudioClip audioClip, IdTag idTag)
	{
		AudioSourceController audioSourceController = this._audioSourcePlayers[idTag].Play(
			audioClip: audioClip
		);

		this.StartCoroutine(
			routine: CoroutineProcessorsCollection.InvokeAfter(
				seconds: audioClip.length,
				action: () =>
				{
					ObjectPool._Instance.Release(audioSourceController);
				}
			)
		);

		return audioSourceController;
	}

	public AudioSourceController Play(AudioClip audioClip, float volume, IdTag idTag)
	{
		AudioSourceController audioSourceController = this._audioSourcePlayers[idTag].Play(
			audioClip: audioClip,
			volume: volume
		);

		this.StartCoroutine(
			routine: CoroutineProcessorsCollection.InvokeAfter(
				seconds: audioClip.length,
				action: () =>
				{
					ObjectPool._Instance.Release(audioSourceController);
				}
			)
		);

		return audioSourceController;
	}

	public AudioSourceController PlayDelayed(AudioClip audioClip, float delay, IdTag idTag)
	{
		AudioSourceController audioSourceController = this._audioSourcePlayers[idTag].PlayDelayed(
			audioClip: audioClip,
			delay: delay
		);

		this.StartCoroutine(
			routine: CoroutineProcessorsCollection.InvokeAfter(
				seconds: audioClip.length + delay,
				action: () =>
				{
					ObjectPool._Instance.Release(audioSourceController);
				}
			)
		);

		return audioSourceController;
	}

	protected override void Awake()
	{
		base.Awake();

		for (int a = 0; a < this._audioPlayerData.Length; a++)
		{
			this._audioSourcePlayers[this._audioPlayerData[a]._IdTag] = this._audioPlayerData[a]._AudioSourcePlayer;
		}
	}

	[Serializable]
	public struct Data
	{
		[SerializeField] private AudioSourcePlayer _audioSourcePlayer;
		public AudioSourcePlayer _AudioSourcePlayer => this._audioSourcePlayer;

		//TODO: Make it immutable.
		[SerializeField] private IdTag _idTag;
		public IdTag _IdTag { get => this._idTag; set => this._idTag = value; }
	}

#if UNITY_EDITOR
	private IdTagsContainer _eoOldIdTagsContainer;

	[SerializeField] private IdTagsContainer _eoAudioIdTagsContainer;
	public IdTagsContainer _AudioIdTagsContainer => this._eoAudioIdTagsContainer;

	private void OnValidate()
	{
		if (this._eoAudioIdTagsContainer != null && this._eoAudioIdTagsContainer != this._eoOldIdTagsContainer)
		{
			for (int a = 0; a < this._audioPlayerData.Length; a++)
			{
				this._audioPlayerData[a]._IdTag = this._eoAudioIdTagsContainer.IdTags[a];
			}

			this._eoOldIdTagsContainer = this._eoAudioIdTagsContainer;
		}
	}

	private void Reset()
	{
		this._eoOldIdTagsContainer = null;
	}
#endif
}
