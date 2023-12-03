using GUARDIANTALES;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class EntityUtility
{
    private static Dictionary<ElementalAttribute, Dictionary<ElementalAttribute, float>>
        elementalDamageMultiplier = new(12);

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
        for (int i = 0; i < (int)ElementalAttribute.MAX; i++)
            elementalDamageMultiplier[(ElementalAttribute)i] = new(2);


        AddElemental(ElementalAttribute.Fire, ElementalAttribute.Water, true);
        AddElemental(ElementalAttribute.Fire, ElementalAttribute.Earth, false);

        AddElemental(ElementalAttribute.Water, ElementalAttribute.Earth, true);
        AddElemental(ElementalAttribute.Water, ElementalAttribute.Fire, false);

        AddElemental(ElementalAttribute.Earth, ElementalAttribute.Fire, true);
        AddElemental(ElementalAttribute.Earth, ElementalAttribute.Water, false);

        AddElemental(ElementalAttribute.Light, ElementalAttribute.Neutral, true);
        AddElemental(ElementalAttribute.Light, ElementalAttribute.Dark, false);

        AddElemental(ElementalAttribute.Dark, ElementalAttribute.Light, true);
        AddElemental(ElementalAttribute.Dark, ElementalAttribute.Neutral, false);

        AddElemental(ElementalAttribute.Neutral, ElementalAttribute.Dark, true);
        AddElemental(ElementalAttribute.Neutral, ElementalAttribute.Light, false);
    }

    public static void AddElemental(ElementalAttribute my, ElementalAttribute other, bool isStrong)
    {
        elementalDamageMultiplier[my][other] = isStrong ? 1.3f : 0.7f;
    }

    /// <summary>
    /// 속성별 데미지 배율을 계산함
    /// 공격받는 입장에서 계산된 데미지임
    /// 
    /// 공격하는 Entity의 Damage에서 사용
    /// </summary>
    // 화 -> 지 -> 수 -> 화
    // 광 -> 암 -> 무 -> 광
    // 유리한 속성일 경우 130% 불리한 경우 70%임
    public static int ModifyDamage(this int damage, ElementalAttribute my, ElementalAttribute other)
    {
        if (elementalDamageMultiplier.Keys.Contains(my) == false) return damage;
        if (elementalDamageMultiplier[my].Keys.Contains(other) == false) return damage;

        return (int)(damage * elementalDamageMultiplier[my][other]);
    }
}