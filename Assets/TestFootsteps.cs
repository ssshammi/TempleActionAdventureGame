using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PixLi;

public class TestFootsteps : MonoBehaviour
{
	[SerializeField] private AudioClipArchive _audioClipArchive;
	public AudioClipArchive _AudioClipArchive => this._audioClipArchive;

	[SerializeField] private float _timeDistance = 0.4f;
	public float _TimeDistance => this._timeDistance;

	private IEnumerator Play()
	{
		while (true)
		{
			AudioPlayer._Instance.Play(this._audioClipArchive.Random(), IdTag.Audio.Footstep);

			yield return new WaitForSeconds(this._timeDistance);
		}
	}

	private void Start()
	{
		this.StartCoroutine(this.Play());
	}
}