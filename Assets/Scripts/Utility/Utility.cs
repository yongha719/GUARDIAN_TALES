using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public static class Utility
{
    private static Random random = new Random();
    
    public static void WrapValue(ref int value, int min, int max)
    {
        if (value < min)
            value = max;

        if (value > max)
            value = min;
    }

    #region Extension Method

    public static void SetActive(this MonoBehaviour @object, bool value)
    {
        @object.gameObject.SetActive(value);
    }

    public static T Random<T>(this IEnumerable<T> source)
    {
        if (source == null)
            throw new ArgumentNullException("source is null");

        int randomIndex = random.Next(0, source.Count());

        return source.ElementAt(randomIndex);
    }
    
    public static T Random<T>(this IEnumerable<T> source, out int index)
    {
        if (source == null)
            throw new ArgumentNullException("source is null");

        index = random.Next(0, source.Count());

        return source.ElementAt(index);
    }

    #endregion
}