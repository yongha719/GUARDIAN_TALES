using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Editor 환경에서 Guardian 인스펙터에 Weapon을 할당할 때 Weapon Type이 다르면 장착하지 못하게 함

namespace GUARDIANTALES
{
    [CustomPropertyDrawer(typeof(WeaponEquipAttribute), true)]
    public class WeaponEquipAttributeDrawer : PropertyDrawer
    {
        private WeaponEquipAttribute Atr => attribute as WeaponEquipAttribute;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);

            if (property.objectReferenceValue != null)
            {
                var weapon = property.objectReferenceValue as Weapon;

                foreach (var weaponType in Atr.WeaponTypes)
                {
                    if (weaponType != weapon.WeaponType)
                        property.objectReferenceValue = null;
                }
            }

        }
    }
}
