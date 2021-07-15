/* Created by Pixel Lifetime */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using PixLi.RandomDistribution;
using Random = PixLi.RandomDistribution.Random;

namespace PixLi
{
    public class TestRandomasdasdasd : MonoBehaviour
    {
		private void Awake()
		{
			Random rnd = new Random();
		
			List<TableData<GameObject>> tableTableData = new List<TableData<GameObject>>()
			{
				new TableData<GameObject>(12.5f, new GameObject("Random Object 12.5")),
				new TableData<GameObject>(16.2f, new GameObject("Random Object 16.2"))
			};

			Table<TableData<GameObject>> innerTable = new Table<TableData<GameObject>>(new Random(rnd.Range(int.MinValue, int.MaxValue)), tableTableData)
			{
				Probability = 20f
			};

			// Double-Dependency Loop.
			List<IRandomDistributionObject<TableData<GameObject>>> tableData = new List<IRandomDistributionObject<TableData<GameObject>>>()
			{
				new TableData<GameObject>(7.5f, new GameObject("Random Object 7.5")),
				new TableData<GameObject>(10, new GameObject("Random Object 10")),
				new TableData<GameObject>(30, new GameObject("Random Object 30")),
				innerTable
			};

			Table<TableData<GameObject>> table = new Table<TableData<GameObject>>(new Random(rnd.Range(int.MinValue, int.MaxValue)), tableData);

			//table.AddEntry(innerTable);

			Debug.Log(table.Select().Object);
			Debug.Log(table.Select().Object);
			Debug.Log(table.Select().Object);
			Debug.Log(table.Select().Object);
			Debug.Log(table.Select().Object);
			Debug.Log(table.Select().Object);
			Debug.Log(table.Select().Object);
			Debug.Log(table.Select().Object);
		}

#if UNITY_EDITOR
		//protected override void OnDrawGizmos()
		//{
		//}
#endif
	}
}

namespace PixLi.UTILITY
{
#if UNITY_EDITOR
    [CustomEditor(typeof(TestRandomasdasdasd))]
    [CanEditMultipleObjects]
    public class TestRandomasdasdasdEditor : Editor
    {
#pragma warning disable 0219, 414
        private TestRandomasdasdasd _sTestRandomasdasdasd;
#pragma warning restore 0219, 414

        private void OnEnable()
        {
            this._sTestRandomasdasdasd = this.target as TestRandomasdasdasd;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
#endif
}