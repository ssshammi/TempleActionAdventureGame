/* Created by Pixel Lifetime */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi.RandomDistribution
{
	public interface IRandomDistributionObject<T>
	{
		float Probability { get; set; }

		bool Selectable { get; set; }
		bool SelectAlways { get; set; }
		bool SelectOncePerQuery { get; set; }

		/// <summary>
		/// If was selected in previous query that contains this object.
		/// </summary>
		bool SelectedInQuery { get; set; }

		T _RandomDistributionObjectValue { get; }
	}
}