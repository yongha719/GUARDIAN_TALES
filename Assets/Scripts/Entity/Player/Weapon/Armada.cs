using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Armada : Weapon
{
    [Space]
    [Header("Armada Skill")]
    public float SkillRadius = 3;

    public GameObject SkillEffect;

    private Collider2D[] collider2Ds = new Collider2D[10];

    private WaitForSeconds skillWaitSeconds = new WaitForSeconds(0.2f);

    protected override void Start()
    {
        base.Start();

        SaveManager.SaveWeapon(spriteRenderer.sprite.texture, name);
    }

    public override void Skill()
    {
        StartCoroutine(SkillCoroutine());
    }

    protected override IEnumerator SkillCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            // 적 스크립트 만들고 적 위치 가져와서 공격하기
            var enemies = Physics2D.OverlapCircleAll(guardianData.Pos, SkillRadius, LayerMask.NameToLayer("Enemy"));

            if (enemies.Length > 0)
            {
                var enemy = enemies.Random(out int index);

                Vector3 enemyPos = enemy.transform.position;

                var effect = Instantiate(SkillEffect, enemyPos, Quaternion.identity);
                // enemies[index].
                
                Destroy(effect, 2f);
            }

            yield return skillWaitSeconds;
        }
    }
}