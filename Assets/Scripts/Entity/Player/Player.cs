using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;


public abstract class Player : MonoBehaviour
{
    [SerializeField]
    protected PlayerData playerData;

    [Tooltip("몇번째 공격인지 나타냄")]
    protected int attackPatternCount;

    protected int AttackPatternCount
    {
        get => attackPatternCount;

        set
        {
            attackPatternCount = value;
            Utility.WrapValue(ref attackPatternCount, 1, 2);
            print(attackPatternCount);
        }
    }


    [field: SerializeField, Tooltip("공격 딜레이")]
    public virtual float AttackDelay { get; protected set; }

    [Tooltip("마지막으로 공격한 시간")]
    protected virtual float lastAttackTime { get; set; }

    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        playerData = GameManager.Instance.PlayerData;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void FixedUpdate()
    {
        // Move
        var dir = InputManager.Instance.Dir;

        transform.Translate(dir * (playerData.Speed.Value * Time.deltaTime));


        bool isLeft = dir.x < 0;

        spriteRenderer.flipX = isLeft;
        playerData.PlayerWeapon.FlipX(isLeft);
    }

    public void TryAttack()
    {
        if (CanAttack())
        {
            AttackPatternCount++;
            Attack();
        }
    }

    protected virtual bool CanAttack()
    {
        if (Time.time - lastAttackTime >= AttackDelay)
        {
            // 바로 공격못하게 막음
            // 공격 애니메이션이 끝나야 쿨타임 시작
            lastAttackTime *= 2;
            return true;
        }

        return false;
    }


    protected abstract void Attack();

    public virtual void TryUseSkill()
    {
        playerData.PlayerWeapon.TryUseSkill();
    }
    
    protected virtual void Skill()
    {
        playerData.PlayerWeapon.Skill();
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