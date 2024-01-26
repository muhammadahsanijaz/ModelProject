using System.Collections.Generic;
using UnityEngine;

namespace MoonKart
{
	public static class IListExtensions
	{
		public static T GetRandom<T>(this IList<T> @this)
		{
			return @this[Random.Range(0, @this.Count)];
		}

		public static void Shuffle<T>(this IList<T> list)
		{
			int n = list.Count - 1;

			while (n > 0)
			{
				int k = Random.Range(0, n);

				T value = list[k];

				list[k] = list[n];
				list[n] = value;

				--n;
			}
		}
	}
}
