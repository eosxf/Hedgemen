using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Hgm.Utilities
{
	public class CachedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		private IDictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
		private IList<TKey> keys = new List<TKey>();
		private IList<TValue> values = new List<TValue>();

		public TValue this[TKey key] { get => dictionary[key]; set => dictionary[key] = value; }

		public ICollection<TKey> Keys => keys;

		public ICollection<TValue> Values => values;

		public int Count => dictionary.Count;

		public bool IsReadOnly => dictionary.IsReadOnly;

		public void Add(TKey key, TValue value)
		{
			dictionary.Add(key, value);
			keys.Add(key);
			values.Add(value);
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			dictionary.Add(item);
			keys.Add(item.Key);
			values.Add(item.Value);
		}

		public void Clear()
		{
			dictionary.Clear();
			keys.Clear();
			values.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return dictionary.Contains(item);
		}

		public bool ContainsKey(TKey key)
		{
			return keys.Contains(key);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			dictionary.CopyTo(array, arrayIndex);
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return dictionary.GetEnumerator();
		}

		public bool Remove(TKey key)
		{
			var kvp = dictionary.SingleOrDefault(e => e.Key.Equals(key));
			if(!dictionary.Contains(kvp)) return false;
			dictionary.Remove(kvp);
			keys.Remove(kvp.Key);
			values.Remove(kvp.Value);
			return true;
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			if(!dictionary.Contains(item)) return false;
			dictionary.Remove(item);
			keys.Remove(item.Key);
			values.Remove(item.Value);
			return true;
		}

		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			return dictionary.TryGetValue(key, out value);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return dictionary.GetEnumerator();
		}
	}
}