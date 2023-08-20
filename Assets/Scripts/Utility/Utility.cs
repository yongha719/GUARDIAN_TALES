using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utility 
{
    public static int WrapValue(ref int value, int min, int max)
    {
        if (value < min)
        {
            value = max;
            return value;
        }

        if (value > max)
        {
            value = min;
            return value;
        }

        return value;
    }
}
