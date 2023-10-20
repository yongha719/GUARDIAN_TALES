using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "GuardianData", menuName = "Data/GuardianData")]
public class GuardianData : EntityData
{
    [NonSerialized]
    public Guardian Guardian;

    private Weapon weapon => Guardian.Weapon;

    public Vector3 Pos => Guardian.transform.position;

    [field: SerializeField, Tooltip("크리티컬 확률")]
    public int CriticalChance { get; set; }

    private int attackDamage => AttackDamageData.Value + weapon.AttackDamage;

    public int CriticalDamage => attackDamage * 2;

    public event Action<int> OnCriticalAttack = _ => { };

    public override int Damage
    {
        get
        {
            if (Random.Range(0, 100) <= CriticalChance)
            {
                OnCriticalAttack(CriticalDamage);
                return CriticalDamage;
            }

            return attackDamage;
        }
    }


    public override void Init()
    {
        base.Init();
    }

    public void InitGuardian(Guardian guardian)
    {
        Guardian = guardian;
    }
}