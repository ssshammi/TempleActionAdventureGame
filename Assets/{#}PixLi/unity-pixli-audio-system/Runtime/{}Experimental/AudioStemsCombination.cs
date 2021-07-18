using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "[Audio Stems Combination]", menuName = "[Audio]/[Audio Stems Combination]")]
public class AudioStemsCombination : ScriptableObject
{
	[SerializeField] private int[] _indices;
	public int[] _Indices => this._indices;
}