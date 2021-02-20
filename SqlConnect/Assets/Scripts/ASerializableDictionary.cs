using System;
using System.Collections;
using System.Collections.Generic;

namespace DefaultNamespace
{
	[System.Serializable]
    public abstract class ASerializableDictionary<TKey, TValue> : ASerializableDictionary, IDictionary<TKey, TValue>
    {
        abstract protected List<TKey> GetKeys();
        abstract protected List<TValue> GetValues();

        protected readonly Dictionary<TKey, TValue> m_Dictionary;

        #region IDictionary

        public int Count { get { return m_Dictionary.Count; } }
        public bool IsReadOnly { get { return false; } }

        public ICollection<TKey> Keys { get { return m_Dictionary.Keys; } }
        public ICollection<TValue> Values { get { return m_Dictionary.Values; } }

        public ASerializableDictionary()
        {
            m_Dictionary = new Dictionary<TKey, TValue>();
        }

        public ASerializableDictionary(IEqualityComparer<TKey> comparer)
        {
            m_Dictionary = new Dictionary<TKey, TValue>(comparer);
        }

        public bool ContainsKey(TKey key)
        {
            return m_Dictionary.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            return m_Dictionary.ContainsValue(value);
        }

        public void Add(TKey key, TValue value)
        {
            m_Dictionary.Add(key, value);
        }

        public void AddOrReplace(TKey key, TValue value)
        {
            if (m_Dictionary.ContainsKey(key))
            {
                m_Dictionary[key] = value;
            }
            else
            {
                m_Dictionary.Add(key, value);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return m_Dictionary.TryGetValue(key, out value);
        }

        public bool Remove(TKey key)
        {
            return m_Dictionary.Remove(key);
        }

        public TValue this[TKey key]
        {
            get { return m_Dictionary[key]; }
            set { m_Dictionary[key] = value; }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return m_Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            m_Dictionary.Add(item.Key, item.Value);
        }

        public virtual void Clear()
        {
            m_Dictionary.Clear();
            GetKeys().Clear();
            GetValues().Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            Dictionary<TKey, TValue>.Enumerator e = m_Dictionary.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.Key.Equals(item.Key) && e.Current.Value.Equals(item.Value))
                {
                    return true;
                }
            }
            e.Dispose();
            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Dictionary<TKey, TValue>.Enumerator e = m_Dictionary.GetEnumerator();
            while (e.MoveNext())
            {
                array[arrayIndex++] = e.Current;
            }
            e.Dispose();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            TValue aux;
            if (m_Dictionary.TryGetValue(item.Key, out aux))
            {
                if (aux != null && aux.Equals(item.Value))
                {
                    m_Dictionary.Remove(item.Key);
                    return true;
                }
            }
            return false;
        }

        #endregion IDictionary

        #region AUnityDictionary
        public override IDictionary<TKey1, TValue1> GetDictionary<TKey1, TValue1>()
        {
            return m_Dictionary as IDictionary<TKey1, TValue1>;
        }

        // Avoid using wherever possible unless you truly don't know what the key type actually is
        // Prefer GetKeyAtIndex() to obtain a concrete type
        public override object GetRawKeyAtIndex(int index)
        {
            return GetKeyAtIndex(index);
        }

        public TKey GetKeyAtIndex(int index)
        {
            List<TKey> keys = GetKeys();
            if (keys != null && keys.Count > index && index >= 0)
            {
                return keys[index];
            }
            return default(TKey);
        }

        // Avoid using wherever possible unless you truly don't know what the value type actually is
        // Prefer GetValueAtIndex() to obtain a concrete type
        public override object GetRawValueAtIndex(int index)
        {
            return GetValueAtIndex(index);
        }

        public TValue GetValueAtIndex(int index)
        {
            List<TValue> values = GetValues();
            if (values != null && values.Count > index && index >= 0)
            {
                return values[index];
            }
            return default(TValue);
        }

        public override Type GetKeyType()
        {
            return typeof(TKey);
        }

        public override Type GetValueType()
        {
            return typeof(TValue);
        }

        public override int GetCount()
        {
            if (GetValues() == null)
            {
                return 0;
            }

            return GetValues().Count;
        }
        #endregion AUnityDictionary

        protected void RefreshDictionary()
        {
            m_Dictionary.Clear();
            List<TKey> keys = GetKeys();
            List<TValue> values = GetValues();
            for (int i = 0; i != keys.Count; ++i)
            {
                if (keys[i] != null && !m_Dictionary.ContainsKey(keys[i]))
                {
                    m_Dictionary.Add(keys[i], values[i]);
                }
            }
        }
    }

    /// <summary>
    /// Abstract class for allowing Unity to have a generic inspector for Dictionary.
    /// </summary>
    public abstract class ASerializableDictionary
    {
        public abstract IDictionary<TKey, TValue> GetDictionary<TKey, TValue>();
        public abstract object GetRawKeyAtIndex(int index);
        public abstract object GetRawValueAtIndex(int index);
        public abstract Type GetKeyType();
        public abstract Type GetValueType();
        public abstract int GetCount();
    }
}