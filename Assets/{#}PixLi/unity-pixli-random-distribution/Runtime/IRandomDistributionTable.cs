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
	public interface IRandomDistributionTable<T> : IRandomDistributionObject<T>
	{
		IEnumerable<T> Select(int selectionQuantity);
		T Select();

		void AddEntry(T @object);
		void AddEntry(IRandomDistributionObject<T> @object);

#if UNITY_EDITOR
#endif
	}
}