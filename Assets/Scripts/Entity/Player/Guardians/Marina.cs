using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Marina : Player
{
    private CapsuleCollider2D circleCollider;

    protected override void Start()
    {
        base.Start();

        circleCollider = GetComponentInChildren<CapsuleCollider2D>();

        playerData.PlayerWeapon.OnEndAttackAnimation += () =>
        {
            AttackCoolDown.InitCoolTime();
        };
    }

    protected override void Attack()
    {
        print("공격");
        playerData.PlayerWeapon.SetAttackAnimator(AttackPatternCount);
    }
}