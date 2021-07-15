/* Created by Pixel Lifetime */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Random = PixLi.RandomDistribution.Random;

using System.Linq;

using SelectionMode = PixLi.RandomDistribution.RandomDistribution.SelectionMode;

namespace PixLi.RandomDistribution
{
	public class RandomDistribution
	{
		public enum SelectionMode { Standard, KeepSelectedOncePerQueryInSample }
	}

	[System.Serializable]
	public class TableData<T> : IRandomDistributionObject<TableData<T>>
	{
		[SerializeField] private float _initialProbability = 1.0f;
		public float InitialProbability
		{
			get => this._initialProbability;
			set => this._initialProbability = value;
		}

		private float _probability = float.NaN;
		public float Probability
		{
			get
			{
				if (this._probability == float.NaN)
				{
					this._probability = this._initialProbability;
				}

				return this._probability;
			}
			set => this._probability = value;
		}

		[SerializeField] private bool _selectable = true;
		public bool Selectable
		{
			get => this._selectable;
			set => this._selectable = value;
		}

		[SerializeField] private bool _selectAlways = false;
		public bool SelectAlways
		{
			get => this._selectAlways;
			set => this._selectAlways = value;
		}

		[SerializeField] private bool _selectOncePerQuery = false;
		public bool SelectOncePerQuery
		{
			get => this._selectOncePerQuery;
			set => this._selectOncePerQuery = value;
		}

		/// <summary>
		/// If was selected in previous query that contains this object.
		/// </summary>
		public bool SelectedInQuery { get; set; }

		public TableData<T> _RandomDistributionObjectValue => this;

		[SerializeField] private T _object;
		public T Object
		{
			get => this._object;
			set => this._object = value;
		}

		public TableData()
		{
			this._selectable = true;
			this._selectAlways = false;
			this._selectOncePerQuery = false;
		}

		public TableData(float initialProbability, T @object) : this()
		{
			this._initialProbability = initialProbability;
			this._object = @object;
		}

		public TableData(float initialProbability, bool selectable, bool selectAlways, bool selectOncePerQuery, T @object) : this(initialProbability, @object)
		{
			this._selectable = selectable;
			this._selectAlways = selectAlways;
			this._selectOncePerQuery = selectOncePerQuery;
		}
	}

	public class Table<TObject> : IRandomDistributionTable<TObject>
		where TObject : IRandomDistributionObject<TObject>
	{
		protected Random random;

		public static IEnumerable<T> Select<T>(Random random, IEnumerable<IRandomDistributionObject<T>> tableData, int selectionQuantity, SelectionMode selectionMode)
			where T : IRandomDistributionObject<T>
		{
			List<IRandomDistributionObject<T>> alwaysSelected = new List<IRandomDistributionObject<T>>();
			List<IRandomDistributionObject<T>> sample = new List<IRandomDistributionObject<T>>();

			float initialSampleProbabilitiesSum = 0f;
			using (IEnumerator<IRandomDistributionObject<T>> tableDataEnumerator = tableData.GetEnumerator())
			{
				while (tableDataEnumerator.MoveNext())
				{
					if (tableDataEnumerator.Current.Selectable)
					{
						tableDataEnumerator.Current.SelectedInQuery = false;

						if (tableDataEnumerator.Current.SelectAlways)
							alwaysSelected.Add(tableDataEnumerator.Current);
						else
						{
							sample.Add(tableDataEnumerator.Current);
							initialSampleProbabilitiesSum += tableDataEnumerator.Current.Probability;
						}
					}
				}
			}

			List<T> selection = new List<T>(alwaysSelected.Select(el => el._RandomDistributionObjectValue));

			float targetProbability = random.Range(0f, initialSampleProbabilitiesSum - Mathf.Epsilon);

			switch (selectionMode)
			{
				case SelectionMode.Standard:

					for (int i = 0; i < selectionQuantity; i++)
					{
						float cumulativeProbability = 0f;

						for (int a = 0; a < sample.Count; a++)
						{
							IRandomDistributionObject<T> sampleData = sample[a];

							cumulativeProbability += sampleData.Probability;
							if (targetProbability < cumulativeProbability)
							{
								selection.Add(sampleData._RandomDistributionObjectValue);

								if (sampleData.SelectOncePerQuery)
								{
									sample.RemoveAt(a);

									targetProbability = random.Range(0f, sample.Sum(el => el.Probability) - Mathf.Epsilon);
								}

								break;
							}
						}
					}

					break;
				case SelectionMode.KeepSelectedOncePerQueryInSample:

					for (int i = 0; i < selectionQuantity; i++)
					{
						float cumulativeProbability = 0f;

						for (int a = 0; a < sample.Count; a++)
						{
							IRandomDistributionObject<T> sampleData = sample[a];

							cumulativeProbability += sampleData.Probability;
							if (targetProbability < cumulativeProbability)
							{
								if (sampleData.SelectOncePerQuery && sampleData.SelectedInQuery)
									continue;

								selection.Add(sampleData._RandomDistributionObjectValue);

								sampleData.SelectedInQuery = true;

								//TODO: fix this selection

								break;
							}
						}
					}

					break;
			}

			return selection;
		}
		public static T Select<T>(Random random, IEnumerable<IRandomDistributionObject<T>> randomDistributionObjects, SelectionMode selectionMode)
			where T : IRandomDistributionObject<T> => Table<TObject>.Select(random, randomDistributionObjects, 1, selectionMode).First();

		public SelectionMode Selection_Mode { get; set; }
		protected IEnumerable<IRandomDistributionObject<TObject>> tableData;

		protected IList<IRandomDistributionObject<TObject>> tableDataListRef;

		public void AddEntry(TObject @object) => this.tableDataListRef.Add(@object);
		public void AddEntry(IRandomDistributionObject<TObject> @object) => this.tableDataListRef.Add(@object);

		//! IRandomDistributionTable

		public IEnumerable<TObject> Select(int selectionQuantity) => Table<TObject>.Select(this.random, this.tableData, selectionQuantity, this.Selection_Mode);
		public TObject Select() => Table<TObject>.Select(this.random, this.tableData, this.Selection_Mode);

		//! IRandomDistributionObject

		public float Probability { get; set; }

		public bool Selectable { get; set; } = true;
		public bool SelectAlways { get; set; }
		public bool SelectOncePerQuery { get; set; }
		public bool SelectedInQuery { get; set; }

		public TObject _RandomDistributionObjectValue => this.Select();

		public Table(
			Random randomDistribution,
			IEnumerable<IRandomDistributionObject<TObject>> tableData,
			SelectionMode selectionMode = SelectionMode.KeepSelectedOncePerQueryInSample)
		{
			this.random = randomDistribution;

			this.tableData = tableData;
			this.tableDataListRef = this.tableData as IList<IRandomDistributionObject<TObject>>;

			this.Selection_Mode = selectionMode;
		}

#if UNITY_EDITOR
#endif
	}
}