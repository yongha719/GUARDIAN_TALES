using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header(nameof(Entity))]
    private int bbracjji;
    
    public abstract EntityData Data { get; set; }

    
    protected virtual void Start()
    {
        print(Data.ElementalAttribute);
    }

    public virtual void Hit(int damage, ElementalAttribute enemyElemental)
    {
        Data.Hp.Value -= ModifyDamage(damage, enemyElemental);
    }

    /// <summary>
    /// 공격받는 입장에서 계산된 데미지임
    /// </summary>
    // 화 -> 지 -> 수 -> 화
    // 광 -> 암 -> 무 -> 광
    // 유리한 속성일 경우 130% 불리한 경우 70%임
    public virtual int ModifyDamage(int damage, ElementalAttribute enemyElemental)
    {
        var damageMultiplier = 1.0f;

        switch (Data.ElementalAttribute)
        {
            case ElementalAttribute.Fire:
                damageMultiplier = enemyElemental switch
                {
                    ElementalAttribute.Water => 1.3f,
                    ElementalAttribute.Earth => 0.7f,
                    _ => 1.0f
                };
                break;
            case ElementalAttribute.Water:
                damageMultiplier = enemyElemental switch
                {
                    ElementalAttribute.Earth => 1.3f,
                    ElementalAttribute.Fire => 0.7f,
                    _ => 1.0f
                };
                break;
            case ElementalAttribute.Earth:
                damageMultiplier = enemyElemental switch
                {
                    ElementalAttribute.Fire => 1.3f,
                    ElementalAttribute.Water => 0.7f,
                    _ => 1.0f
                };
                break;
            case ElementalAttribute.Light:
                damageMultiplier = enemyElemental switch
                {
                    ElementalAttribute.Neutral => 1.3f,
                    ElementalAttribute.Dark => 0.7f,
                    _ => 1.0f
                };
                break;
            case ElementalAttribute.Dark:
                damageMultiplier = enemyElemental switch
                {
                    ElementalAttribute.Light => 1.3f,
                    ElementalAttribute.Neutral => 0.7f,
                    _ => 1.0f
                };
                break;
            case ElementalAttribute.Neutral:
                damageMultiplier = enemyElemental switch
                {
                    ElementalAttribute.Dark => 1.3f,
                    ElementalAttribute.Light => 0.7f,
                    _ => 1.0f
                };
                break;
        }

        return (int)(damage * damageMultiplier);
    }
}