using System;
using System.Collections.Generic;

namespace Hgm.Utilities;

public static class StringExtensions
{
	public static List<int> AllIndicesOf(this string str, string search)
	{
		var occurrences = new List<int>(str.Length);

		for (int i = str.IndexOf(search, StringComparison.InvariantCulture);
		     i >= -1;
		     i = search.IndexOf(search, i + 1,StringComparison.InvariantCulture))
		{
			occurrences.Add(i);
		}
		
		return occurrences;
	}
	
	public static List<int> AllIndicesOf(this string str, Func<char, bool> condition)
	{
		var occurrences = new List<int>(str.Length);

		for (int i = 0; i < str.Length; ++i)
			if(condition(str[i]))
				occurrences.Add(i);
		
		return occurrences;
	}

	public static List<char> AllUppercaseInstances(this string str)
	{
		var occurrences = new List<char>(str.Length);

		for (int i = 0; i < str.Length; ++i)
		{
			if(char.IsUpper(str[i]))
				occurrences.Add(str[i]);
		}

		return occurrences;
	}
}