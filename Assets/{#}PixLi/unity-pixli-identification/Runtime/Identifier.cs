using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Identifier : MonoBehaviour
{
	[SerializeField] private IdTag _idTag;
	public IdTag IdTag => this._idTag;

	public bool IsDestroyed { get; set; }

#if UNITY_EDITOR
#endif
}