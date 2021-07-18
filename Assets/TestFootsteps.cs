using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PixLi;

public class TestFootsteps : MonoBehaviour
{
	[SerializeField] private AudioClipArchive _audioClipArchive;
	public AudioClipArchive _AudioClipArchive => this._audioClipArchive;

	[SerializeField] private Cooldown _cooldown;
	public Cooldown _Cooldown => this._cooldown;

	private void Start()
	{
		this.StartCoroutine(
			routine: CoroutineProcessorsCollection.InvokeIndefinitely(
				customYieldInstruction: this._cooldown,
				action: () =>
				{
					this._cooldown.Reset();

					AudioPlayer._Instance.Play(this._audioClipArchive.Random(), IdTag.Audio.Footstep);
				}
			)
		);
	}
}