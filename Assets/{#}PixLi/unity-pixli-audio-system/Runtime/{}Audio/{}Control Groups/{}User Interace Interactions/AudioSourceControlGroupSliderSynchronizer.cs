using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AudioSourceControlGroupSliderSynchronizer : MonoBehaviour
{
	[SerializeField] private AudioSourceControlGroup _audioSourceControlGroup;
	public AudioSourceControlGroup _AudioSourceControlGroup => this._audioSourceControlGroup;

	[SerializeField] private Slider _slider;
	public Slider _Slider => this._slider;

	public void Synchronize() => this._slider.value = this._audioSourceControlGroup.Volume;

	private UnityAction<float> _setSliderValueUnityAction;

	private void Awake()
	{
		this.Synchronize();

		this._setSliderValueUnityAction = new UnityAction<float>((value) => this._slider.value = value);

		this._audioSourceControlGroup._OnVolumeChange.AddListener(
			call: this._setSliderValueUnityAction
		);
	}

	private void OnDestroy()
	{
		this._audioSourceControlGroup._OnVolumeChange.RemoveListener(
			call: this._setSliderValueUnityAction
		);
	}

#if UNITY_EDITOR
	private void Reset()
	{
		this._slider = this.GetComponent<Slider>();
	}
#endif
}