using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public Data<int> Hp;

    public Data<int> Speed = new();

    public Data<int> AttackDamage = new();

    public Data<int> Armor;

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
        Hp = new(hp => Hp.Value /= 100 / (100 + Armor.Value));

        Armor = new(onArmorChange);
    }

    private void onHpChange(int hp)
    {
        Hp.Value /= (100 + Armor.Value) / 100;
    }

    private void onArmorChange(int armor)
    {
    }
}