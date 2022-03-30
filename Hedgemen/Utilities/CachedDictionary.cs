using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hgm.Utilities;

[Serializable]
public class CachedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
	private readonly IDictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
	private readonly IList<TKey> keys = new List<TKey>();
	private readonly IList<TValue> values = new List<TValue>();

	public TValue this[TKey key]
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => dictionary[key];
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set => dictionary[key] = value;
	}

	public ICollection<TKey> Keys => keys;

	public ICollection<TValue> Values => values;

	public int Count => dictionary.Count;

	public bool IsReadOnly => dictionary.IsReadOnly;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Add(TKey key, TValue value)
	{
		dictionary.Add(key, value);
		keys.Add(key);
		values.Add(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Add(KeyValuePair<TKey, TValue> item)
	{
		dictionary.Add(item);
		keys.Add(item.Key);
		values.Add(item.Value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Clear()
	{
		dictionary.Clear();
		keys.Clear();
		values.Clear();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Contains(KeyValuePair<TKey, TValue> item)
	{
		return dictionary.Contains(item);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool ContainsKey(TKey key)
	{
		return keys.Contains(key);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
	{
		dictionary.CopyTo(array, arrayIndex);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
	{
		return dictionary.GetEnumerator();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Remove(TKey key)
	{
		var kvp = dictionary.SingleOrDefault(e => e.Key.Equals(key));
		if (!dictionary.Contains(kvp)) return false;
		dictionary.Remove(kvp);
		keys.Remove(kvp.Key);
		values.Remove(kvp.Value);
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Remove(KeyValuePair<TKey, TValue> item)
	{
		if (!dictionary.Contains(item)) return false;
		dictionary.Remove(item);
		keys.Remove(item.Key);
		values.Remove(item.Value);
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
	{
		return dictionary.TryGetValue(key, out value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return dictionary.GetEnumerator();
	}
}