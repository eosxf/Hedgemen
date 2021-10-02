using System.Collections.Generic;

namespace Hgm.Utilities
{
	public static class DictionaryExtensions
	{
		public static TV Get<TK, TV>(this IDictionary<TK, TV> self, TK key)
		{
			TV val = default;
			self.TryGetValue(key, out val);
			return val;
		}
	}
}