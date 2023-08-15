using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Data<T>
{
    public event Action<T> OnChange = _ => {};

    [SerializeField]
    private T value;

    public T Value
    {
        get => value;

        set
        {
            this.value = value;

            OnChange(value);
        }
    }
    
    public Data(){}

    public Data(T _value)
    {
        value = _value;
    }
    
    public Data(Action<T> onChange)
    {
        OnChange += onChange;
    }

    public Data(T _value, Action<T> onChange)
    {
        value = _value;
        OnChange += onChange;
    }
}