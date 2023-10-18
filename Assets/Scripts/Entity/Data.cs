using System;
using UnityEngine;
    
[Serializable]
public class Data<T> where T : struct, IComparable, IConvertible
{
    public event Action<T> OnChange = _ => { };

    [SerializeField]
    private T value;

    public T Value
    {
        get => value;

        set
        {
            this.value = value;

            OnChange(this.value);
        }
    }

    public Data() { }

    public Data(T _value)
    {
        value = _value;
    }

    public Data(Action<T> _onChange)
    {
        OnChange += _onChange;
    }

    public Data(T _value, Action<T> _onChange)
    {
        value = _value;
        OnChange += _onChange;
    }

    public static T operator +(Data<T> data) => data.Value;

    public static T operator -(Data<T> data)
    {
        if (data.value is int or float)
        {
            dynamic result = data.Value;
            return -result;
        }
        else
        {
            throw new System.NotSupportedException("this operator supported only int or float");
        }
    }

    public static Data<T> operator *(Data<T> data, T other)
    {
        if (data.value is int or float)
        {
            dynamic value = data.value;
            dynamic otherValue = other;

            return value * otherValue;
        }
        else
        {
            throw new System.NotSupportedException("this operator supported only int or float");
        }
    }

    public static Data<T> operator +(Data<T> data, T other)
    {
        if (data.value is int or float)
        {
            dynamic value = data.value;
            dynamic otherValue = other;

            return value + otherValue;
        }
        else
        {
            throw new System.NotSupportedException("this operator supported only int or float");
        }
    }

    public static Data<T> operator -(Data<T> data, T other)
    {
        if (data.value is int or float)
        {
            dynamic value = data.value;
            dynamic otherValue = other;

            return value - otherValue;
        }
        else
        {
            throw new System.NotSupportedException("this operator supported only int or float");
        }
    }


    public static Data<T> operator /(Data<T> data, T other)
    {
        if (data.value is int or float)
        {
            dynamic value = data.value;
            dynamic otherValue = other;

            return value / otherValue;
        }
        else
        {
            throw new System.NotSupportedException("this operator supported only int or float");
        }
    }

    public static implicit operator int(Data<T> data)
    {
        if (data.value is int or float)
        {
            dynamic value = data.value;

            return value;
        }
        else
        {
            throw new System.NotSupportedException("this operator supported only int or float");
        }
    }

    public static implicit operator float(Data<T> data)
    {
        if (data.value is int or float)
        {
            dynamic value = data.value;

            return value;
        }
        else
        {
            throw new System.NotSupportedException("this operator supported only int or float");
        }
    }
}