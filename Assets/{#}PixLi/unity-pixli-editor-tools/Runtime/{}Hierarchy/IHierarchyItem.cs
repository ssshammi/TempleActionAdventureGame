using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public interface IHierarchyItem
{
#if UNITY_EDITOR
	//TODO: comment all of this.
	/// <summary>
	/// 
	/// </summary>
	/// <param name="selectionID"></param>
	/// <param name="selectionRect"></param>
	void OnHierarchyDraw(int selectionID, Rect selectionRect);
#endif
}