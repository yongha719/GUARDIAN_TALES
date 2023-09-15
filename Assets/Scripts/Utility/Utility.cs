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

    /// <summary>
    /// 주어진 value 값을 min과 max 사이의 범위로 클램핑한 후, 그 값을 0과 1 사이의 값으로 정규화함
    /// </summary>
    /// <param name="min">범위의 최소값</param>
    /// <param name="max">범위의 최대값</param>
    /// <param name="value">클램핑하고 정규화할 값</param>
    /// <returns>클램핑 및 정규화된 값 (0과 1 사이)</returns>
    public static float Clamp01(float min, float max, float value)
    {
        // 상대적인 위치 계산
        float relativePosition = (value - min) / (max - min);

        // 0과 1 사이의 범위로 매핑
        float mappedValue = Mathf.Clamp01(relativePosition);

        return mappedValue;
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