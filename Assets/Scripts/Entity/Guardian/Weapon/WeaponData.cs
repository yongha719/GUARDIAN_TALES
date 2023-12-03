using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUARDIANTALES
{
    public class WeaponData
    {
        public Data<int> AttackDamage = new Data<int>();

        public WeaponType WeaponType;

        [SerializeField]
        protected ElementalAttribute elementalAttribute;

        public ElementalAttribute ElementalAttribute => elementalAttribute;

        protected int MAX_CRITICAL_CHANCE = 15;
    }
}