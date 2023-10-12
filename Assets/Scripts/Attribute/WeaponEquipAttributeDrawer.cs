using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(WeaponEquipAttribute), true)]
public class WeaponEquipAttributeDrawer : PropertyDrawer
{
    private WeaponEquipAttribute Atr => attribute as WeaponEquipAttribute;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        base.OnGUI(position, property, label);

        if (property.objectReferenceValue == null)
            return;

        var weapon = property.objectReferenceValue as Weapon;

        if(Atr.WeaponType != weapon.WeaponType)
        {
            property.objectReferenceValue = null;
        }
    }
}
