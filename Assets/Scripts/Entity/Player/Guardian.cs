using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;


public abstract class Guardian : Entity, IGuardianActions
{
    public override EntityData Data
    {
        get => guardianData;
        set => guardianData = (GuardianData)value;
    }

    [Header(nameof(Guardian))]
    [SerializeField]
    protected GuardianData guardianData;

    [Tooltip("몇번째 공격인지 나타냄")]
    protected int attackPatternCount;

    protected int AttackPatternCount
    {
        get => attackPatternCount;

        set
        {
            attackPatternCount = value;
            Utility.WrapValue(ref attackPatternCount,
                minAttackCount, maxAttackCount);
            print(attackPatternCount);
        }
    }

    protected virtual int minAttackCount { get; }

    protected virtual int maxAttackCount { get; }

    public virtual bool HasAdditionalSkill => false;

    public CooldownController AttackCoolDown = new();

    public CooldownController AdditionalSkillCoolDown = new();

    #region Unity Components

    protected SpriteRenderer spriteRenderer;

    #endregion

    protected override void Start()
    {
        guardianData = GameManager.Instance.GuardianData;

        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        print("가디언");
    }

    protected virtual void FixedUpdate()
    {
        // Move
        var dir = InputManager.Instance.Dir;

        transform.Translate(dir * (guardianData.Speed.Value * Time.deltaTime));

        bool isLeft = dir.x < 0;

        spriteRenderer.flipX = isLeft;
        guardianData.PlayerWeapon.FlipX(isLeft);

        AttackCoolDown.OnCooldownReady += () =>
        {
            AttackPatternCount++;
            Attack();
        };
    }

    protected abstract void Attack();

    public bool TryAttack()
    {
        return AttackCoolDown.IsCooldownFinished();
    }


    public bool TryUseSkill(out float coolTime)
    {
        coolTime = guardianData.PlayerWeapon.SkillCoolDown.Delay;

        return guardianData.PlayerWeapon.TryUseSkill();
    }

    public bool TryUseAdditionalSKill(out float coolTime)
    {
        coolTime = AdditionalSkillCoolDown.Delay;

        return AdditionalSkillCoolDown.IsCooldownFinished();
    }


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        print($"{other.name} : 감지");

        if (other.CompareTag("Enemy"))
        {
            print("적 공격");
        }
    }
}