using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class GuardianData : EntityData
{
    [NonSerialized]
    public Guardian Guardian;

    [SerializeField]
    private Weapon playerWeapon;
    public Weapon PlayerWeapon
    {
        get => playerWeapon;

        set
        {
            if (WeaponType == value.WeaponType)
            {
                Debug.Log("장착 가능");
            }
            else
            {
                Debug.Log("장착 불가능");
            }
        }
    }

    public WeaponType WeaponType;

    public Vector3 Pos => Guardian.transform.position;

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
    }

    public void WeaponEquip(Weapon weapon)
    {
        if (PlayerWeapon != null)
        {
            PlayerWeapon.UnEquip();
        }

        PlayerWeapon = weapon;
        PlayerWeapon.Init(this);
    }

    public void WeaponUnEquip()
    {
        Destroy(PlayerWeapon.gameObject);

        PlayerWeapon = null;
    }
}