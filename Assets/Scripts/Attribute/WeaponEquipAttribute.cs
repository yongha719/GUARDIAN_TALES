using UnityEngine;


namespace GUARDIANTALES
{
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public class WeaponEquipAttribute : PropertyAttribute
    {

        public WeaponType[] WeaponTypes { get; set; }

        public WeaponEquipAttribute(params WeaponType[] weaponTypes)
        {
            WeaponTypes = weaponTypes;
        }
    }
}
