using System.Collections;
using System.Collections.Generic;
using PixLi.RandomDistribution;
using UnityEngine;
using Random = PixLi.RandomDistribution.Random;

namespace PixLi
{
	[System.Serializable]
	public class RandomDistributionProvider<TProvidedData> : Provider<TableData<TProvidedData>>
	{
		[SerializeField] private List<TableData<TProvidedData>> _data;
		public List<TableData<TProvidedData>> _Data => this._data;

		[System.NonSerialized]
		private Table<TableData<TProvidedData>> _table;

		[Range(0.0f, 1.0f)]
		[SerializeField] private float[] _probabilityRatioPerPreviouslyProvidedData = new float[0];
		private TableData<TProvidedData>[] _previouslyProvidedData;

		private Random _defaultRandom = new Random();

		public override TableData<TProvidedData> Provide()
		{
			if (this._table == null)
			{
				this._previouslyProvidedData = new TableData<TProvidedData>[this._probabilityRatioPerPreviouslyProvidedData.Length];

				this._table = new Table<TableData<TProvidedData>>(
					randomDistribution: Random._GlobalRandom,
					tableData: this._data
				);
			}

			TableData<TProvidedData> providedData = this._table.Select();

			TableData<TProvidedData> lastPreviouslyProvidedData = this._previouslyProvidedData[this._previouslyProvidedData.Length - 1];

			if (lastPreviouslyProvidedData != null)
			{
				lastPreviouslyProvidedData.Probability = lastPreviouslyProvidedData.InitialProbability;

				//Debug.LogError(lastPreviouslyProvidedData.Probability);
			}

			this._previouslyProvidedData.StepForward(1);
			this._previouslyProvidedData[0] = providedData;

			for (int a = 0; a < this._previouslyProvidedData.Length; a++)
			{
				if (this._previouslyProvidedData[a] != null)
				{
					//Debug.Log($"Probability: {this._previouslyProvidedData[a].Probability} | Object: {this._previouslyProvidedData[a].Object}");

					this._previouslyProvidedData[a].Probability = this._previouslyProvidedData[a].InitialProbability * this._probabilityRatioPerPreviouslyProvidedData[a];
				}
			}

			return providedData;
		}

		public void Initialize(Random random)
		{
			this._previouslyProvidedData = new TableData<TProvidedData>[this._probabilityRatioPerPreviouslyProvidedData.Length];

			this._table = new Table<TableData<TProvidedData>>(
				randomDistribution: random,
				tableData: this._data
			);

			//TODO: Kay, so you need this because when you change probabilities, it remembers the probability itself even after exiting play mode. Initial probability doesn't work because it's not set on start. Why? Because it remembers private values as well, so _probability isn't NAN at the beginning as it should be.

			for (int a = 0; a < this._data.Count; a++)
			{
				this._data[a].Probability = this._data[a].InitialProbability;
			}
		}
	}
}