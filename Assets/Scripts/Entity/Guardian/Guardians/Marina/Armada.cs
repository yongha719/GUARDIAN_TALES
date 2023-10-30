using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 마리나 전용무기 \
/// 한손검 \
/// 수속성
/// </summary>
public class Armada : Weapon
{
    [Space]
    [Header("Armada Skill")]
    public float SkillRadius = 3;

    public GameObject SkillEffect;

    private Collider2D[] rayCastColliders = new Collider2D[10];

    [Tooltip("스킬 쓸 때 ")]
    private float skillAttackDelay;

    [SerializeField, Tooltip("무기 데미지에 곱할 스킬 데미지")]
    private float skillDamageMultiply;

    private float skillDamage => skillDamageMultiply * (AttackDamage + guardianData.Damage);

    protected override void Start()
    {
        base.Start();

        SaveManager.SaveWeaponImage(spriteRenderer.sprite.texture, name);
    }

    public override void Attack()
    {
        SetAttackAnimator(AttackPatternCount);
        attackTaskCancelToken.Cancel();


    }

    public override void Skill()
    {
        SkillAsync().Forget();
    }

    protected override async UniTaskVoid SkillAsync()
    {
        for (int i = 0; i < 10; i++)
        {
            var enemies = Physics2D.OverlapCircleAll(guardianData.Pos, SkillRadius, LayerMask.NameToLayer("Enemy"));

            if (enemies.Length > 0)
            {
                var enemy = enemies.GetRandomElement().GetComponent<Enemy>();

                Vector3 enemyPos = enemy.transform.position;

                //var effect = Instantiate(SkillEffect, enemyPos, Quaternion.identity);
                //Destroy(effect, 2f);

                enemy.Data.HpData -= guardianData.Damage;
            }

            print($"is there enemy : {enemies.Length > 0}");

            await UniTask.Delay((int)(skillAttackDelay * 1000));
        }
    }
}