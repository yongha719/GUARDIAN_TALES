using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum ElementalAttribute
{
    Fire,
    Water,
    Earth,
    Light,
    Dark,
    Neutral
}

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : EntityData
{
    [NonSerialized]
    public Player Player;

    public Weapon PlayerWeapon;

    [field: SerializeField, Tooltip("크리티컬 확률")]
    public int CriticalChance { get; set; }

    public int CriticalDamage => AttackDamage.Value * 2;

    public event Action<int> OnCriticalAttack;

    public override int Damage
    {
        get
        {
            if (Random.Range(0, 100) <= CriticalChance)
            {
                OnCriticalAttack(CriticalDamage);
                return CriticalDamage;
            }

            return AttackDamage.Value;
        }
    }


    public override void Init()
    {
        base.Init();
        
        Player = FindObjectOfType<Player>();
    }

    public void WeaponChange(Weapon weapon)
    {
        PlayerWeapon = weapon;
        
        weapon.Init(this);
    }
}