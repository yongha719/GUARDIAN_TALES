using UnityEngine;

public class Marina : Guardian
{
    [Header(nameof(Marina))]
    private CapsuleCollider2D capsuleCollider;

    protected override int minAttackCount => 1;
    protected override int maxAttackCount => 2;

    
    public override bool HasAdditionalSkill => true;

    protected override void Start()
    {
        base.Start();

        capsuleCollider = GetComponentInChildren<CapsuleCollider2D>();

        guardianData.PlayerWeapon.OnEndAttackAnimation += () =>
        {
            AttackCoolDown.InitCoolTime();
        };
    }

    protected override void Attack()
    {
        print("공격");
        guardianData.PlayerWeapon.SetAttackAnimator(AttackPatternCount);
    }

    protected override void AdditionalSkill()
    {   
        
    }
}