using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
public class WeaponEquipAttribute : PropertyAttribute
{
    public WeaponType WeaponType { get; set; }

    public WeaponEquipAttribute(WeaponType weaponType)
    {
        WeaponType = weaponType;
    }
}
