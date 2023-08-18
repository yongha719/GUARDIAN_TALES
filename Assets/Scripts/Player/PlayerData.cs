using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public Player Player;

    public Vector2 MoveDirection;
    
    public Data<int> Hp;

    public Data<int> Speed = new Data<int>();

    public Data<int> AttackDamage = new Data<int>();

    public Data<int> Armor = new Data<int>();

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

        Hp = new Data<int>(onHpChange);
    }

    private void onHpChange(int hp)
    {
        Hp.Value /= (100 + Armor.Value) / 100;
    }

}