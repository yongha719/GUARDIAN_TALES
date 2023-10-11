using Cysharp.Threading.Tasks;
using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CooldownController
{
    [field: SerializeField]
    public float Delay { get; set; }

    [Tooltip("처음 이벤트를 실행할 때 딜레이없이 바로 쓸 수 있는지")]
    public bool CanUseImmediately;

    [Tooltip("이벤트를 실행하고 바로 쓸 수 있는지")]
    public bool hasCooldown;

    private float lastCoolTime;

    private bool hasFillImage;

    private Image fillImage;

    public float RemainingCooldown
    {
        get
        {
            if (Time.time - lastCoolTime > Delay)
                return 0;

            return (Time.time - lastCoolTime);
        }
    }

    public float RemainingCooldownClamp01 => Utility.Clamp01(0, Delay, RemainingCooldown);

    public event Action OnCoolDownReady = () => { };


    public CooldownController()
    {
    }

    public void InitFillImage(Image image)
    {
        hasFillImage = true;
        fillImage = image;
    }

    public void InitCoolTime()
    {
        lastCoolTime = CanUseImmediately ? -Delay : Time.time;
    }

    private async UniTaskVoid FillImageUpdateAsync()
    {
        while (RemainingCooldown != 0)
        {
            fillImage.fillAmount = RemainingCooldownClamp01;

            await UniTask.NextFrame();
        }
    }

    /// <summary>
    /// 쿨타임 됐는지 체크하고 OnCooldownReady 실행
    /// </summary>
    /// <returns></returns>
    public bool TryCoolDownAction()
    {
        var coolTime = Time.time - lastCoolTime;

        if (coolTime >= Delay)
        {
            Debug.Log("실행");
            lastCoolTime = hasCooldown ? Time.time : Delay * 10;

            OnCoolDownReady();
            return true;
        }

        return false;
    }
}