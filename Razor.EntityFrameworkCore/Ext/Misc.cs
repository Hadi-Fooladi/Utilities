using System;
using System.Collections.Generic;

namespace HaFT.Utilities.Razor.EntityFrameworkCore;

static class MiscExt
{
	public static int FindIndex<T>(this IEnumerable<T> list, Predicate<T> match)
	{
		if (list is List<T> l) return l.FindIndex(match);

		int ndx = 0;
		foreach (var item in list)
		{
			if (match(item))
				return ndx;

			ndx++;
		}

		return -1;
	}
}
