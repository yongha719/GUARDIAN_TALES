using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armada : Weapon
{
    [Space]
    [Header("Armada Skill")]
    public float SkillRadius = 3;

    public GameObject SkillEffect;

    protected override void Start()
    {
        base.Start();

        SaveManager.Instance.SaveWeapon(spriteRenderer.sprite.texture, name);
    }

    public override void Skill()
    {
        for (int i = 0; i < 10; i++)
        {
            // 적 스크립트 만들고 적 위치 가져와서 공격하기
            // if() { }
            // else
            var pos = transform.position + Random.onUnitSphere * SkillRadius;

            var effect = Instantiate(SkillEffect, pos, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }
}