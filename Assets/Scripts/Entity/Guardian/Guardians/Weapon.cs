using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum AnimatorParameterType
{
    Float,
    Integer,
    Bool,
    Trigger
}

public enum WeaponType
{
    OneHandedSword,
    TwoHandedSword,
    Rifle,
    Bow,
    Basket,
    Staff,
    Claw,
    Shield
}

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    protected GuardianData guardianData;

    public WeaponType WeaponType;

    public float AttackDamage;

    public bool isEquiped => guardianData != null;

    [Space]
    public CooldownController SkillCoolDown;

    [SerializeField]
    private string attackTriggerName = "Attack";

    [SerializeField]
    private string attackPatternCountName = "AttackPatternCount";

    /// <summary>
    /// 공격 애니메이션 끝났을 때 콜백
    /// </summary>
    public event Action OnEndAttackAnimation = () => { };

    private Animator animator;
    protected SpriteRenderer spriteRenderer;

    public void Init(GuardianData guardianData)
    {
        this.guardianData = guardianData;
    }
    
    protected virtual void Start()
    {
        // Test
        guardianData = GameManager.Instance.GuardianData;
        guardianData.PlayerWeapon = this;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        SkillCoolDown.InitCoolTime();
        SkillCoolDown.OnCoolDownReady += () =>
        {
            Skill();
        };
    }

    public void Equip(Guardian guardian)
    {
        guardianData = (GuardianData)guardian.Data;
    }

    public void UnEquip()
    {
        guardianData = null;
    }

    public abstract void Skill();

    protected virtual async UniTask SkillTask()
    {
        await UniTask.Delay(1000);
    }

    public bool TryUseSkill()
    {
        print("try use skill");
        
        if (SkillCoolDown.IsCooldownFinished())
        {
            print("use Skill");
            Skill();
            return true;
        }

        return false;
    }

    public void FlipX(bool isLeft)
    {
        transform.rotation = Quaternion.Euler(0, isLeft ? 180 : 0, 0);
    }

    public void Destroy()
    {
        Destroy(gameObject);
        Dictionary<int, int> da = new();
        da.First();
    }

    // 자식개체에 있는 WeaponChildColliderHandler 스크립트에서 호출
    public void OnChildTriggerEnter2D(Collider2D other)
    {
        print("무기 공격");
    }

    #region Animator Event에서 쓸 함수들

    public void ResetTrigger()
    {
        animator.ResetTrigger(attackTriggerName);
    }

    public void EndAttackAnimation()
    {
        OnEndAttackAnimation();
        ResetTrigger();
    }

    /// <summary>
    /// 플레이어 스크립트에서 공격할 때 사용할 함수
    /// </summary>
    /// <param name="value">공격 패턴 카운트</param>
    public void SetAttackAnimator(int value)
    {
        SetAnimatorParameter(AnimatorParameterType.Trigger, attackTriggerName);
        SetAnimatorParameter(AnimatorParameterType.Integer, attackPatternCountName, value);
    }

    /// <summary>
    /// 플레이어에서 호출할 Animator Parameter 설정할 함수
    /// </summary>
    public void SetAnimatorParameter(AnimatorParameterType parameterType, string parameterName,
        object value = null)
    {
        if (parameterType != AnimatorParameterType.Trigger && value == null)
            throw new NullReferenceException($"{nameof(SetAnimatorParameter)} Method : Parameter value is Null");

        switch (parameterType)
        {
            case AnimatorParameterType.Float:
                animator.SetFloat(parameterName, (float)value);
                break;
            case AnimatorParameterType.Integer:
                animator.SetInteger(parameterName, (int)value);
                break;
            case AnimatorParameterType.Bool:
                animator.SetBool(parameterName, (bool)value);
                break;
            case AnimatorParameterType.Trigger:
                animator.SetTrigger(parameterName);
                break;
        }
    }


    #endregion
}