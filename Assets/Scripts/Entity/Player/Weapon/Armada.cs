using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armada : Weapon
{
    public float SkillRadius = 3;
    public GameObject SkillEffect;

    public override void Skill()
    {
        for (int i = 0; i < 10; i++)
        {
            var pos = transform.position + Random.onUnitSphere * SkillRadius;

            var effect = Instantiate(SkillEffect, pos, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }
}