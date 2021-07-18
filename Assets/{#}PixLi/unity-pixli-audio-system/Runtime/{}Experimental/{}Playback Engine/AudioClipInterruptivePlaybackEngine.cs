using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipInterruptivePlaybackEngine : MonoBehaviour
{
	[SerializeField] private AudioSourcePlayer _audioSourcePlayer;
	public AudioSourcePlayer _AudioSourcePlayer => this._audioSourcePlayer;

	private AudioSourceController _activeAudioSourceController;

	public void Play(AudioClip audioClip) => this._activeAudioSourceController.Play(audioClip: audioClip);
	public void Stop() => this._activeAudioSourceController.Stop();

	private void Start()
	{
		this._activeAudioSourceController = this._audioSourcePlayer.AquireAudioSourceController();
	}
}