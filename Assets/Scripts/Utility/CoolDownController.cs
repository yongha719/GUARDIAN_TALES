using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CooldownController
{
    [field: SerializeField]
    public float Delay { get; set; }

    private float lastCoolTime = float.MaxValue;

    private bool hasFillImage;

    private Image fillImage;

    public float RemainingCooldown
    {
        get
        {
            if (Time.time - lastCoolTime > Delay)
                return 0;

            return Delay - (Time.time - lastCoolTime);
        }
    }

    public float RemainingCooldownClamp01 => Utility.Clamp01(0, Delay, RemainingCooldown);
    
    public event Action OnCooldownReady = () => { };

    
    public CooldownController() { }

    public CooldownController(float _delay)
    {
        Delay = _delay;
    }

    public void InitFillImage(Image image)
    {
        hasFillImage = true;
        fillImage = image;
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