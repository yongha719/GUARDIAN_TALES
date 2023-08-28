using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CooldownController
{
    [field: SerializeField]
    public float Delay { get; set; }

    private float lastCoolTime = float.MaxValue;

    public event Action OnCooldownReady = () => { };

    public CooldownController() { }

    public CooldownController(float delay)
    {
        Delay = delay;
    }

    public void InitCoolTime()
    {
        lastCoolTime = Time.time;
    }

    public bool IsCooldownFinished()
    {
        if (Time.time - lastCoolTime >= Delay)
        {
            lastCoolTime *= 2;
            return true;
        }

        return false;
    }
}