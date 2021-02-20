using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
	[System.Serializable]
	public class SerializableDictionary<TKey, TValue> : ASerializableDictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[SerializeField]
		private List<TKey> m_Keys = new List<TKey>();

		[SerializeField]
		private List<TValue> m_Values = new List<TValue>();

		protected override List<TKey> GetKeys() => m_Keys;
		protected override List<TValue> GetValues() => m_Values;

		public SerializableDictionary() : base() { }

		public SerializableDictionary(IEqualityComparer<TKey> comparer) : base(comparer){}

#region ISerializationCallbackReceiver
		public void OnBeforeSerialize()
		{
			// we don't convert dictionary values into the arrays
			// since we always work on the lists when in the inspector
		}

		public void OnAfterDeserialize()
		{
			RefreshDictionary();
		}
#endregion ISerializationCallbackReceiver

		/// <summary>
		/// Manually save the dictionary into it's internal lists.
		/// </summary>
		public void SaveData()
		{
			m_Keys.Clear();
			m_Values.Clear();

			Dictionary<TKey, TValue>.Enumerator e = m_Dictionary.GetEnumerator();
			while (e.MoveNext())
			{
				m_Keys.Add(e.Current.Key);
				m_Values.Add(e.Current.Value);
			}
			e.Dispose();
		}

		protected void OnValidate()
		{
			RefreshDictionary();
		}
	}
}