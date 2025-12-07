using UnityEngine;
using System.Collections.Generic;

namespace Random{
public static class RandomExtensions
{
    public static T Random<T>(this T[] array)
    {
        if (array == null || array.Length == 0)
            return default;

        return array[UnityEngine.Random.Range(0, array.Length)];
    }

    public static T Random<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
            return default;

        return list[UnityEngine.Random.Range(0, list.Count)];
    }
}
}