using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PixLi;

public class AudioStemsController : MonoBehaviourSingleton<AudioStemsController>
{
	[SerializeField] private AudioClipArchive _audioClipArchive;
	public AudioClipArchive _AudioClipArchive => this._audioClipArchive;

	//TODO: Do something about this.
	private AudioSourceController[] _audioSourceControllers;
	private bool[] _isAudioSourceControllerActive;

	[SerializeField] private AudioSourcePlayer _audioSourcePlayer;
	public AudioSourcePlayer _AudioSourcePlayer => this._audioSourcePlayer;

	[SerializeField] private AnimationCurve _fadeInCurve = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 0.0f);
	public AnimationCurve _FadeInCurve => this._fadeInCurve;

	[SerializeField] private AnimationCurve _fadeOutCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public AnimationCurve _FadeOutCurve => this._fadeOutCurve;

	[SerializeField] private float _fadeTime = 1.0f;
	public float _FadeTime => this._fadeTime;

	public void Play(int index)
	{
		this._audioSourceControllers[index].UnMute();
		this._audioSourceControllers[index].Fade(
			time: this._fadeTime,
			animationCurve: this._fadeOutCurve
		);

		this._isAudioSourceControllerActive[index] = true;
	}

	public void Stop(int index)
	{
		this._audioSourceControllers[index].Fade(
			time: this._FadeTime,
			animationCurve: this._fadeInCurve,
			onFinished: () =>
			{
				this._audioSourceControllers[index].Mute();
			}
		);

		this._isAudioSourceControllerActive[index] = false;
	}

	public void PlayCombination(AudioStemsCombination audioStemsCombination)
	{
		for (int a = 0; a < this._audioSourceControllers.Length; a++)
		{
			if (!audioStemsCombination._Indices.Contains(a))
				this.Stop(a);
		}

		//TODO: You wouldn't really have to do this, if you resolved this situation in fade process. But not really, what if you really want to restart fading?
		for (int a = 0; a < audioStemsCombination._Indices.Length; a++)
		{
			if (!this._isAudioSourceControllerActive[audioStemsCombination._Indices[a]])
				this.Play(audioStemsCombination._Indices[a]);
		}
	}

	private void Start()
	{
		this._audioSourceControllers = new AudioSourceController[this._audioClipArchive.Assets.Count];
		this._isAudioSourceControllerActive = new bool[this._audioSourceControllers.Length];

		for (int a = 0; a < this._audioClipArchive.Assets.Count; a++)
		{
			//this._audioSourceControllers.Add(AudioPlayer._Instance.Play(audioClip: this._audioClipArchive.Assets[a].Object, idTag: this._audioPlayerType));

			AudioSourceController audioSourceController = this._audioSourcePlayer.Play(
				audioClip: this._audioClipArchive.Assets[a].Object
			);

			audioSourceController.Mute();
			audioSourceController.Fade(
				time: this._fadeTime,
				animationCurve: this._fadeInCurve
			); //? It's not really necessary to do Fade with the way fading works but just to be sure that there isn't a spike when it's FadesOut.
			audioSourceController.AudioSource.loop = true; //? We can modify a state of pulled entry because we are never releasing it.

			this._audioSourceControllers[a] = audioSourceController;
		}
	}

#if UNITY_EDITOR
	[Header("Editor Only")]
	[SerializeField] private int _eoPieceIndex;

	[Button]
	public void Play() => this.Play(this._eoPieceIndex);

	[Button]
	public void Stop() => this.Stop(this._eoPieceIndex);

	[SerializeField] private AudioStemsCombination _eoAudioStemsCombination;

	[Button]
	public void PlayCombination() => this.PlayCombination(this._eoAudioStemsCombination);
#endif
}