using UnityEngine;
using System.Collections.Generic;

namespace MYK
{
	public static class ListUtils
	{
        public static T GetRandom<T>(this List<T> list)
        {
            if (list == null || list.Count < 1)
            {
                throw new System.ArgumentException("Invalid list.");
            }

            return list[Random.Range(0, list.Count)];
        }
	}
}