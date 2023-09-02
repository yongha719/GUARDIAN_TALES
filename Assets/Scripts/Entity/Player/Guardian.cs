using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;


public abstract class Guardian : Entity
{
    public override EntityData Data
    {
        get => guardianData;
        set => guardianData = (GuardianData)value;
    }
    
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
    
    [field: SerializeField]
    public int AttackDelay { get; set; }
    
    [field: SerializeField]
    public int AdditionalSkillDelay { get; set; }
    
    public CooldownController AttackCoolDown;

    public CooldownController AdditionalSkillCoolDown;
    
    protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        guardianData = GameManager.Instance.GuardianData;
    }

    protected virtual void Start()
    {
        base.Start();

        AttackCoolDown = new CooldownController(AttackDelay);
        AdditionalSkillCoolDown = new CooldownController(AdditionalSkillDelay);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void FixedUpdate()
    {
        // Move
        var dir = InputManager.Instance.Dir;

        transform.Translate(dir * (guardianData.Speed.Value * Time.deltaTime));

        bool isLeft = dir.x < 0;

        spriteRenderer.flipX = isLeft;
        guardianData.PlayerWeapon.FlipX(isLeft);

        AttackCoolDown.OnCooldownReady += Attack;
    }

    public void TryAttack()
    {
        if (AttackCoolDown.IsCooldownFinished())
        {
            AttackPatternCount++;
        }
    }


    protected abstract void Attack();

    public virtual void TryUseSkill()
    {
        guardianData.PlayerWeapon.TryUseSkill();
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