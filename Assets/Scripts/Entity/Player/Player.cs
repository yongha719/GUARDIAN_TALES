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

    public CooldownController AttackCoolDown;

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
        if (AttackCoolDown.IsCooldownFinished())
        {
            AttackPatternCount++;
            Attack();
        }
    }


    protected abstract void Attack();

    public virtual void TryUseSkill()
    {
        playerData.PlayerWeapon.TryUseSkill();
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