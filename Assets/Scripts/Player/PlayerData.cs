using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PlayerElementalAttribute
{
    Fire,
    Water,
    Earth,
    Light,
    Dark,
    Neutral
}

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    [NonSerialized]
    public Player Player;

    public Weapon PlayerWeapon;
    
    public Data<int> Hp;

    public Data<int> Speed = new Data<int>();

    public Data<int> AttackDamage = new Data<int>();

    public Data<int> Armor = new Data<int>();

    [SerializeField]
    private PlayerElementalAttribute playerElementalAttribute;

    public PlayerElementalAttribute PlayerElementalAttribute => playerElementalAttribute;

    [field: SerializeField, Tooltip("크리티컬 확률")]
    public int CriticalChance { get; set; }

    public int CriticalDamage => AttackDamage.Value * 2;

    public event Action<int> OnCriticalAttack;

    public int Damage
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


    public void Init()
    {
        Player = FindObjectOfType<Player>();

        Hp = new Data<int>((hp) =>
        {
            Hp.Value = (int)(Hp.Value * (100f / (100 + Armor.Value)));
        });
    }

    public void WeaponChange<T>(T weapon) where T : Weapon
    {
        PlayerWeapon = weapon;
        
    }
}