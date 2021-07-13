/* Created by Robots */

using System;
using System.Linq;
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

public partial struct IdTag
{
	public static class Audio
	{
		public static IdTag Ambience = new IdTag("Ambience");
		public static IdTag Music = new IdTag("Music");
		public static IdTag SoundEffect = new IdTag("SoundEffect");
	}
}