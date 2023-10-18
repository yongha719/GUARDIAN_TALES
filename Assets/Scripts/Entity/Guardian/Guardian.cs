using Cysharp.Threading.Tasks;
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

    [Header(nameof(Guardian))]
    [SerializeField]
    protected GuardianData guardianData;

    [SerializeField, WeaponEquip(WeaponType.OneHandedSword)]
    private Weapon weapon;

    public Weapon Weapon
    {
        get => weapon;

        set
        {
            if (WeaponType == value.WeaponType)
            {
                weapon = value;
            }
            else
            {
                Debug.Log("장착 불가능");
            }
        }
    }

    public WeaponType WeaponType;


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

    [Space]
    public CooldownController AttackCoolDown = new();

    public CooldownController AdditionalSkillCoolDown = new();

    #region Unity Components

    protected SpriteRenderer spriteRenderer;

    #endregion

    protected override void Start()
    {
        guardianData.InitGuardian(this);

        Weapon.Equip(this);

        AttackCoolDown.InitCoolTime();
        AdditionalSkillCoolDown.InitCoolTime();

        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        print("가디언");

        AttackCoolDown.OnCoolDownReady += () =>
        {
            AttackPatternCount++;
            Attack();
        };

        AdditionalSkillCoolDown.OnCoolDownReady += () =>
        {
            AdditionalSkill();
            print("Additional Skill OnCoolDownReady");
        };
    }

    protected virtual void FixedUpdate()
    {
        // Move
        var dir = InputManager.Instance.Dir;

        transform.Translate(dir * (guardianData.SpeedData * Time.deltaTime));

        bool isLeft = dir.x < 0;

        spriteRenderer.flipX = isLeft;
        Weapon.FlipX(isLeft);
    }

    public void WeaponEquip(Weapon weapon)
    {
        if (Weapon != null)
        {
            Weapon.UnEquip();
        }

        Weapon = weapon;
        Weapon.Equip(this);
    }

    public void WeaponUnEquip()
    {
        Destroy(Weapon.gameObject);

        Weapon = null;
    }


    protected abstract void Attack();

    protected virtual void AdditionalSkill() {
        AddtionalSkillAsync().Forget();
    }

    protected virtual async UniTaskVoid AddtionalSkillAsync() { }

    public bool TryAttack()
    {
        return AttackCoolDown.TryCoolDownAction();
    }


    public bool TryUseSkill(out float coolTime)
    {
        coolTime = Weapon.SkillCoolDown.Delay;

        return Weapon.TryUseSkill();
    }

    public bool TryUseAdditionalSKill(out float coolTime)
    {
        coolTime = AdditionalSkillCoolDown.Delay;

        return AdditionalSkillCoolDown.TryCoolDownAction();
    }


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        print($"{other.name} : 감지");

        if (other.CompareTag("Enemy"))
        {
            print("적 충돌");
        }
    }
}