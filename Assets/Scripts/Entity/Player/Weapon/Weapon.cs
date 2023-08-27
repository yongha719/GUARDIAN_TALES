using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAnimatorParameterType
{
    Float,
    Integer,
    Bool,
    Trigger
}

public abstract class Weapon : MonoBehaviour
{
    private PlayerData playerData;

    [SerializeField]
    private string attackTriggerName = "Attack";

    [SerializeField]
    private string attackPatternCountName = "AttackPatternCount";

        
    [field: SerializeField]
    public virtual float SkillDelay { get; protected set; }

    [Tooltip("마지막으로 스킬을 사용한 시간")]
    protected virtual float lastUseSkillTime { get; set; }
    
    /// <summary>
    /// 공격 애니메이션 끝났을 때 콜백
    /// </summary>
    public event Action OnEndAttackAnimation = () =>
    {
    };

    private Animator animator;
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        // Test
        playerData = GameManager.Instance.PlayerData;
        playerData.PlayerWeapon = this;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Init(PlayerData _playerData)
    {
        playerData = _playerData;
    }

    /// <summary>
    /// 플레이어 스크립트에서 공격할 때 사용할 함수
    /// </summary>
    /// <param name="value">공격 패턴 카운트</param>
    public void SetAttackAnimator(int value)
    {
        SetAnimatorParameter(WeaponAnimatorParameterType.Trigger, attackTriggerName);
        SetAnimatorParameter(WeaponAnimatorParameterType.Integer, attackPatternCountName, value);
    }

    /// <summary>
    /// 플레이어에서 호출할 Animator Parameter 설정할 함수
    /// </summary>
    public void SetAnimatorParameter(WeaponAnimatorParameterType parameterType, string parameterName,
        object value = null)
    {
        if (parameterType != WeaponAnimatorParameterType.Trigger && value == null)
            throw new NullReferenceException("SetAnimatorParameter Method : Parameter valu is Null");

        switch (parameterType)
        {
            case WeaponAnimatorParameterType.Float:
                animator.SetFloat(parameterName, (float)value);
                break;
            case WeaponAnimatorParameterType.Integer:
                animator.SetInteger(parameterName, (int)value);
                break;
            case WeaponAnimatorParameterType.Bool:
                animator.SetBool(parameterName, (bool)value);
                break;
            case WeaponAnimatorParameterType.Trigger:
                animator.SetTrigger(parameterName);
                break;
        }
    }

    public abstract void Skill();
    
    public void TryUseSkill()
    {
        if (CanUseSkill())
            Skill();
    }

    protected virtual bool CanUseSkill()
    {
        if (Time.time - lastUseSkillTime >= SkillDelay)
        {
            // 바로 스킬 사용못하게 막음
            // 스킬이 끝나야 쿨타임 시작
            lastUseSkillTime *= 2;
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

    #endregion
}