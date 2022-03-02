using System.Collections.Generic;

namespace Hgm.Utilities;

public static class DictionaryExtensions
{
	public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key)
	{
		self.TryGetValue(key, out var val);
		return val;
	}
}