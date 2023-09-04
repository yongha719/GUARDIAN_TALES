using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header(nameof(Entity))]
    private int bbracjji;
    
    private Dictionary<ElementalAttribute, Dictionary<ElementalAttribute, float>> elementalDamageMultiplier = new Dictionary<ElementalAttribute, Dictionary<ElementalAttribute, float>>();

    public abstract EntityData Data { get; set; }

    
    protected virtual void Start()
    {
        print(Data.ElementalAttribute);
        Add(ElementalAttribute.Fire, ElementalAttribute.Water, true);
        Add(ElementalAttribute.Fire, ElementalAttribute.Earth, false);

        Add(ElementalAttribute.Water, ElementalAttribute.Earth, true);
        Add(ElementalAttribute.Water, ElementalAttribute.Fire, false);

        Add(ElementalAttribute.Earth, ElementalAttribute.Fire, true);
        Add(ElementalAttribute.Earth, ElementalAttribute.Water, false);

        Add(ElementalAttribute.Light, ElementalAttribute.Neutral, true);
        Add(ElementalAttribute.Light, ElementalAttribute.Dark, false);

        Add(ElementalAttribute.Dark, ElementalAttribute.Light, true);
        Add(ElementalAttribute.Dark, ElementalAttribute.Neutral, false);

        Add(ElementalAttribute.Neutral, ElementalAttribute.Dark, true);
        Add(ElementalAttribute.Neutral, ElementalAttribute.Light, false);
    }

    public virtual void Hit(int damage, ElementalAttribute enemyElemental)
    {
        Data.Hp.Value -= ModifyDamage(damage, enemyElemental);
    }

    private void Add(ElementalAttribute my, ElementalAttribute other, bool isStrong)
    {
        elementalDamageMultiplier[my][other] = isStrong ? 1.3f : 0.7f;
    }

    /// <summary>
    /// 공격받는 입장에서 계산된 데미지임
    /// </summary>
    // 화 -> 지 -> 수 -> 화
    // 광 -> 암 -> 무 -> 광
    // 유리한 속성일 경우 130% 불리한 경우 70%임
    public virtual int ModifyDamage(int damage, ElementalAttribute enemyElemental)
    {
        if (!elementalDamageMultiplier.Keys.Contains(Data.ElementalAttribute)) return 1;
        if (!elementalDamageMultiplier[Data.ElementalAttribute].Keys.Contains(enemyElemental)) return 1;

        return (int)(damage * elementalDamageMultiplier[Data.ElementalAttribute][enemyElemental]);
    }
}