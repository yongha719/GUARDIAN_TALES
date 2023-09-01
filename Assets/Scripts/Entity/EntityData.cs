using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementalAttribute
{
    Fire,
    Water,
    Earth,
    Light,
    Dark,
    Neutral
}

[CreateAssetMenu(fileName = "EntityData", menuName = "Data/EntityData")]
public class EntityData : ScriptableObject
{
    public string Name;
    
    public Data<int> Hp;

    public Data<int> Speed = new Data<int>();

    public Data<int> AttackDamage = new Data<int>();

    public Data<int> Armor = new Data<int>();

    [SerializeField]
    protected ElementalAttribute elementalAttribute;

    public ElementalAttribute ElementalAttribute => elementalAttribute;

    public virtual int Damage => AttackDamage.Value;

    public virtual void Init()
    {
        Hp = new Data<int>((hp) =>
        {
            Hp.Value = (int)(Hp.Value * (100f / (100 + Armor.Value)));
        });
    }
}
