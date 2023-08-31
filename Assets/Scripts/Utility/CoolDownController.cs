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

    /// <summary>
    /// 쿨타임 됐는지 체크하고 OnCooldownReady 실행
    /// </summary>
    /// <returns></returns>
    public bool IsCooldownFinished()
    {
        if (Time.time - lastCoolTime >= Delay)
        {
            lastCoolTime *= 2;

            OnCooldownReady();
            return true;
        }

        return false;
    }
}