using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceController : MonoBehaviour, IPoolable
{
	[SerializeField] private AudioSourceControlGroup _audioSourceControlGroup;
	public AudioSourceControlGroup _AudioSourceControlGroup => this._audioSourceControlGroup;

	public AudioSource AudioSource { get; private set; }

	private float _fadeVolumeMultiplier = 1.0f;

	private float _volume;
	public float Volume
	{
		get => this.AudioSource.volume;
		set
		{
			this._volume = value;

			this.AudioSource.volume = this._volume * this._fadeVolumeMultiplier * this._audioSourceControlGroup.Volume;
		}
	}

	//TODO: fix this ugly code.
	private void UpdateVolume(float volume) => this.Volume = this._volume;

	public void Play(AudioClip audioClip)
	{
		this.AudioSource.volume = this._volume * this._audioSourceControlGroup.Volume;
		this.AudioSource.clip = audioClip;

		this.AudioSource.Play();
	}

	public void Play(AudioClip audioClip, float volume)
	{
		this._volume = volume;
		this.AudioSource.volume = this._volume * this._audioSourceControlGroup.Volume;
		this.AudioSource.clip = audioClip;

		this.AudioSource.Play();
	}

	public void PlayDelayed(AudioClip audioClip, float delay)
	{
		this.AudioSource.volume = this._volume * this._audioSourceControlGroup.Volume;
		this.AudioSource.clip = audioClip;

		this.AudioSource.PlayDelayed(delay: delay);
	}

	public void Pause() => this.AudioSource.Pause();
	public void Resume() => this.AudioSource.UnPause();
	public void Stop() => this.AudioSource.Stop();

	public void Mute() => this.AudioSource.mute = true;
	public void UnMute() => this.AudioSource.mute = false;

	//TODO: Think of a more sophisticated system for fading.
	//TODO: Think of what to do when fading is happening and other functions like Play and Stop are called.

	//TODO: Replace it with cooldowns.
	//TODO: Do something about these processes and Actions. Do you really have to do it each time. Also reminder that you can use chaining for subscriptions and stuff like that. Already make your tweening system.
	private IEnumerator FadeProcess(float targetTime, AnimationCurve animationCurve, Action onFinished)
	{
		float time = 0.0f;

		while (time < targetTime)
		{
			//TODO: You could have 1 fade process, they are pretty similar. Just have a different evaluation function. As you can see - differnt curve works great. But remember that you would maybe need a different evaludation function, not only curve, so better abstract that and use curve outside as part of evalution function.
			this._fadeVolumeMultiplier = animationCurve.Evaluate(time: time / targetTime);

			this.UpdateVolume(this._volume);

			time += Time.unscaledDeltaTime;

			yield return null;
		}

		this._fadeVolumeMultiplier = animationCurve.keys[animationCurve.length - 1].value; // Last key, last value as final value.

		this.UpdateVolume(this._volume);

		if (onFinished != null)
			onFinished.Invoke();
	}

	public void Fade(float time, AnimationCurve animationCurve, Action onFinished = null)
	{
		this.StopAllCoroutines();
		this.StartCoroutine(this.FadeProcess(targetTime: time, animationCurve: animationCurve, onFinished: onFinished));
	}

	public GameObject GameObject => this.gameObject;

	public ObjectPool.Pool Pool { get; set; }

	public void OnRelease()
	{
		this._audioSourceControlGroup._OnVolumeChange.RemoveListener(
			call: this.UpdateVolume
		);

		this._audioSourceControlGroup._MasterAudioSourceControlGroup._OnVolumeChange.RemoveListener(
			call: this.UpdateVolume
		);
	}

	public void OnAquire()
	{
		this._audioSourceControlGroup._OnVolumeChange.AddListener(
			call: this.UpdateVolume
		);
		
		this._audioSourceControlGroup._MasterAudioSourceControlGroup._OnVolumeChange.AddListener(
			call: this.UpdateVolume
		);
	}

	private void Awake()
	{
		this.AudioSource = this.GetComponent<AudioSource>();

		this.Volume = this.AudioSource.volume;
	}
}
