using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marina : Player
{
    private CapsuleCollider2D circleCollider;

    
    public override float AttackDelay => 0.3f;

    protected override void Start()
    {
        base.Start();

        circleCollider = GetComponentInChildren<CapsuleCollider2D>();

        playerData.PlayerWeapon.OnEndAttackAnimation += () =>
        {
            lastAttackTime = Time.time;
        };
    }

    protected override void Attack()
    {
        print("공격");
        playerData.PlayerWeapon.SetAttackAnimator(AttackPatternCount);
    }
}