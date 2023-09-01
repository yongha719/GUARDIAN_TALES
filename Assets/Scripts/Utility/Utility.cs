using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utility 
{
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

    #endregion
}
