using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUARDIANTALES
{
    public abstract class Guardian : Entity
    {
        public override EntityData Data
        {
            get => guardianData;
            set => guardianData = (GuardianData)value;
        }

        [Header(nameof(Guardian))]
        [SerializeField]
        protected GuardianData guardianData;

        [SerializeField, WeaponEquip(WeaponType.OneHandedSword)]
        private Weapon weapon;

        public Weapon Weapon
        {
            get => weapon;

            set
            {
                if (WeaponType == value.WeaponType)
                {
                    weapon = value;
                }
                else
                {
                    Debug.Log("장착 불가능");
                }
            }
        }

        public WeaponType WeaponType;

        [Tooltip("공격할 때 자동 이동가능한 거리")]
        public float AutoMovableDistanceAttacking;

        /// <summary> 근접 유닛인지 </summary>
        public virtual bool IsProximityUnit { get; }

        [Tooltip("몇번째 공격인지 나타냄")]
        protected int attackPatternCount;

        protected int AttackPatternCount
        {
            get => attackPatternCount;

            set
            {
                attackPatternCount = value;
                Utility.WrapValue(ref attackPatternCount,
                    minAttackCount, maxAttackCount);
                print(attackPatternCount);
            }
        }

        protected virtual int minAttackCount { get; }

        protected virtual int maxAttackCount { get; }

        public bool HasAdditionalSkill => this is IGuardianAdditionalSkill;

        [Space]
        [Header("CoolDown Controllers")]
        public CooldownController AttackCoolDown = new();

        public CooldownController AdditionalSkillCoolDown = new();

        #region Unity Components

        protected SpriteRenderer spriteRenderer;

        #endregion

        protected override void Start()
        {
            guardianData.InitGuardian(this);

            Weapon.Equip(this);

            AttackCoolDown.InitCoolTime();
            AdditionalSkillCoolDown.InitCoolTime();

            base.Start();

            spriteRenderer = GetComponent<SpriteRenderer>();

            AttackCoolDown.OnCoolDownReady += () =>
            {
                AttackPatternCount++;
                Attack();
            };

            if (this is IGuardianAdditionalSkill additionalSkill)
            {
                AdditionalSkillCoolDown.OnCoolDownReady += () =>
                {
                    additionalSkill.AdditionalSkill();
                };
            }
        }

        protected virtual void FixedUpdate()
        {
            // Move
            var dir = InputManager.Instance.Dir;

            //transform.Translate(dir * (guardianData.SpeedData * Time.deltaTime));

            bool isLeft = dir.x < 0;

            spriteRenderer.flipX = isLeft;
            Weapon.FlipX(isLeft);
        }

        public void WeaponEquip(Weapon weapon)
        {
            if (Weapon != null)
            {
                Weapon.UnEquip();
            }

            Weapon = weapon;
            Weapon.Equip(this);
        }

        public void WeaponUnEquip()
        {
            Destroy(Weapon.gameObject);

            Weapon = null;
        }


        protected abstract void Attack();

        public bool TryAttack()
        {
            return AttackCoolDown.TryCoolDownAction();
        }


        public bool TryUseSkill(out float coolTime)
        {
            coolTime = Weapon.SkillCoolDown.Delay;

            return Weapon.TryUseSkill();
        }

        public bool TryUseAdditionalSKill(out float coolTime)
        {
            coolTime = AdditionalSkillCoolDown.Delay;

            return AdditionalSkillCoolDown.TryCoolDownAction();
        }

        /// <summary>
        /// 근접 공격 유닛들이 공격할 때 가까운 거리에 있는 적에게 이동함
        /// <br></br>
        /// 두가지 이동 방법
        /// <br></br>
        /// 1. 일정 거리내에 적이 있으면 바로 적앞까지 전진 
        /// <br></br>
        /// 2. 거리가 멀면 적 쪽으로 조금씩 전진
        /// 
        /// </summary>
        protected void MoveToNearestEnemy()
        {
            var nearestEnemy = GameManager.Instance.GetNearestEnemyPosition();

            var enemyDistance = Vector3.Distance(transform.position, nearestEnemy);

            if (enemyDistance < 3)
            {
                Move(nearestEnemy, 0.2f);
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                // 충돌 데미지라 약함
                Hit(enemy);
            }
        }
    }
}