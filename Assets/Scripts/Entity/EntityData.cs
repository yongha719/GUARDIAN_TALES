using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUARDIANTALES
{
    public enum ElementalAttribute
    {
        Fire,
        Water,
        Earth,
        Light,
        Dark,
        Neutral,
        MAX
    }

    [CreateAssetMenu(fileName = "EntityData", menuName = "Data/EntityData")]
    public class EntityData : ScriptableObject
    {
        public string Name;

        public Data<int> HpData;

        public Data<int> SpeedData = new Data<int>();

        public Data<int> AttackDamageData = new Data<int>();

        public Data<int> ArmorData = new Data<int>();

        [SerializeField]
        protected ElementalAttribute elementalAttribute;

        public ElementalAttribute ElementalAttribute => elementalAttribute;

        public virtual int Damage => AttackDamageData.Value;

        public virtual void Init()
        {
            HpData = new Data<int>((hp) =>
            {
                HpData.Value = (int)(HpData.Value * (100f / (100 + ArmorData.Value)));
            });
        }
    }
}
