using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hgm.Register;

public sealed class ObjectRegistry<TValue> : IReadOnlyDictionary<NamespacedString, TValue>
{
	private readonly Dictionary<NamespacedString, TValue> _registries;

	public ObjectRegistry()
		: this(1337)
	{
		
	}
	
	public ObjectRegistry(int initialCapacity)
	{
		_registries = new Dictionary<NamespacedString, TValue>(initialCapacity);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public IEnumerator<KeyValuePair<NamespacedString, TValue>> GetEnumerator() => _registries.GetEnumerator();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public int Count
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _registries.Count;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool ContainsKey(NamespacedString key) => _registries.ContainsKey(key);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool TryGetValue(NamespacedString key, out TValue value) => _registries.TryGetValue(key, out value);

	public TValue this[NamespacedString key]
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _registries[key];
	}

	public IEnumerable<NamespacedString> Keys
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _registries.Keys;
	}

	public IEnumerable<TValue> Values
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _registries.Values;
	}

	public TValue Get(NamespacedString key)
	{
		bool registriesHasKey = _registries.TryGetValue(key, out var value);

		if (!registriesHasKey) 
			return default;

		return value;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TValue Get<T>(NamespacedString key) where T : TValue => (T)Get(key);

	public bool Register(NamespacedString key, TValue value)
	{
		if (ContainsKey(key))
			return false;
		if (value is null)
			return false;
		
		_registries.Add(key, value);
		return true;
	}
}

public sealed class ObjectRegistry : IReadOnlyDictionary<NamespacedString, object>
{
	private readonly Dictionary<NamespacedString, object> _registries;

	public ObjectRegistry()
		: this(1337)
	{
		
	}
	
	public ObjectRegistry(int initialCapacity)
	{
		_registries = new Dictionary<NamespacedString, object>(initialCapacity);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public IEnumerator<KeyValuePair<NamespacedString, object>> GetEnumerator() => _registries.GetEnumerator();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public int Count
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _registries.Count;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool ContainsKey(NamespacedString key) => _registries.ContainsKey(key);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool TryGetValue(NamespacedString key, out object value) => _registries.TryGetValue(key, out value);

	public object this[NamespacedString key]
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _registries[key];
	}

	public IEnumerable<NamespacedString> Keys
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _registries.Keys;
	}

	public IEnumerable<object> Values
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _registries.Values;
	}

	public TValue Get<TValue>(NamespacedString key)
	{
		bool registriesHasKey = _registries.TryGetValue(key, out var value);

		if (!registriesHasKey) 
			return default;

		return (TValue)value;
	}

	public bool Register(NamespacedString key, object value)
	{
		if (ContainsKey(key))
			return false;
		if (value is null)
			return false;
		
		_registries.Add(key, value);
		return true;
	}
}