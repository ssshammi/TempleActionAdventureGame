using System;
using System.Collections;
using System.Collections.Generic;
using PixLi;
using UnityEngine;

public class AudioSourcePlayer : MonoBehaviour
{
	[SerializeField] private AudioSourceController _audioSourceControllerPrefab;
	public AudioSourceController _AudioSourceControllerPrefab => this._audioSourceControllerPrefab;

	public AudioSourceController Play(AudioClip audioClip)
	{
		AudioSourceController audioSourceController = ObjectPool._Instance.Aquire<AudioSourceController>(
			poolable: this._audioSourceControllerPrefab
		);

		audioSourceController.Play(audioClip: audioClip);

		return audioSourceController;
	}

	public void PlayUI(AudioClip audioClip)
	{
		AudioSourceController audioSourceController = this.Play(audioClip: audioClip);

		//TODO: Some extension like that could prove handy for Object Pooling, like self releasing thingy after some seconds.
		audioSourceController.StartCoroutine(
			routine: CoroutineProcessorsCollection.InvokeAfter(
				seconds: audioClip.length,
				action: () =>
				{
					ObjectPool._Instance.Release(audioSourceController);
				}
			)
		);
	}

	public void PlayUI(AudioClipArchive audioClipArchive)
	{
		AudioClip audioClip = audioClipArchive.Random();

		AudioSourceController audioSourceController = this.Play(audioClip: audioClip);

		//TODO: Some extension like that could prove handy for Object Pooling, like self releasing thingy after some seconds.
		audioSourceController.StartCoroutine(
			routine: CoroutineProcessorsCollection.InvokeAfter(
				seconds: audioClip.length,
				action: () =>
				{
					ObjectPool._Instance.Release(audioSourceController);
				}
			)
		);
	}

	public AudioSourceController Play(AudioClip audioClip, float volume)
	{
		AudioSourceController audioSourceController = ObjectPool._Instance.Aquire<AudioSourceController>(
			poolable: this._audioSourceControllerPrefab
		);

		audioSourceController.Play(
			audioClip: audioClip,
			volume: volume
		);

		return audioSourceController;
	}

	public AudioSourceController PlayDelayed(AudioClip audioClip, float delay)
	{
		AudioSourceController audioSourceController = ObjectPool._Instance.Aquire<AudioSourceController>(
			poolable: this._audioSourceControllerPrefab
		);

		audioSourceController.PlayDelayed(
			audioClip: audioClip,
			delay: delay
		);

		return audioSourceController;
	}

	public AudioSourceController AquireAudioSourceController()
	{
		return ObjectPool._Instance.Aquire<AudioSourceController>(
			poolable: this._audioSourceControllerPrefab
		);
	}
}
