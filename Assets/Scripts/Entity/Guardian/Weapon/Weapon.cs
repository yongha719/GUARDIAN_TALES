using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace GUARDIANTALES
{
    public enum AnimatorParameterType
    {
        Float,
        Integer,
        Bool,
        Trigger
    }

    public enum WeaponType
    {
        OneHandedSword,
        TwoHandedSword,
        Rifle,
        Bow,
        Basket,
        Staff,
        Claw,
        Shield
    }


    public abstract class Weapon : MonoBehaviour
    {
        [Header("Weapon")]
        protected GuardianData guardianData;

        public WeaponType WeaponType;

        public int AttackDamage;

        public bool IsEquiped => guardianData != null;

        [Tooltip("몇번째 공격인지 나타냄")]
        protected int attackPatternCount;

        protected int AttackPatternCount
        {
            get => attackPatternCount;

            set
            {
                attackPatternCount = value;
                Utility.WrapValue(ref attackPatternCount, minAttackCount, maxAttackCount);
            }
        }

        [field: SerializeField]
        protected virtual int minAttackCount { get; set; }

        [field: SerializeField]
        protected virtual int maxAttackCount { get; set; }

        /// <summary> 공격 패턴 초기화되는 시간 공격하고 일정시간 지나면 초기화 </summary>
        [field: SerializeField]
        protected virtual float attackPatternInitTime { get; set; }

        [Space]
        public CooldownController SkillCoolDown;

        [SerializeField, Tooltip("애니메이터 파라미터 이름")]
        private string attackTriggerName = "Attack";

        [SerializeField, Tooltip("애니메이터 파라미터 이름")]
        private string attackPatternCountName = "AttackPatternCount";

        protected CancellationTokenSource attackTaskCancelToken = new();

        /// <summary>
        /// 공격 애니메이션 끝났을 때 콜백
        /// </summary>
        public event Action OnEndAttackAnimation = () => { };

        private Animator animator;
        protected SpriteRenderer spriteRenderer;

        protected virtual void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            SkillCoolDown.InitCoolTime();
            SkillCoolDown.OnCoolDownReady += () =>
            {
                Skill();
            };
        }

        public void Equip(Guardian guardian)
        {
            guardianData = (GuardianData)guardian.Data;
        }

        public void UnEquip()
        {
            guardianData = null;
        }

        public abstract void Attack();

        protected virtual async UniTask AttackAsync() { }

        protected virtual async UniTaskVoid InitAttackPatternCount()
        {

        }

        public abstract void Skill();

        protected virtual async UniTaskVoid SkillAsync() { }

        public bool TryUseSkill()
        {
            if (SkillCoolDown.TryCoolDownAction())
            {
                print("use Skill");
                return true;
            }

            return false;
        }

        public void FlipX(bool isLeft)
        {
            transform.rotation = Quaternion.Euler(0, isLeft ? 180 : 0, 0);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// 자식개체에 있는 WeaponChildColliderHandler 스크립트에서 호출
        /// <see cref="WeaponChildColliderHandler.OnTriggerEnter2D(Collider2D)"/>
        /// </summary>
        public virtual void OnChildTriggerEnter2D(Collider2D other)
        {
            print("무기 공격");
        }

        #region Animator Event에서 쓸 함수들

        public void ResetTrigger()
        {
            animator.ResetTrigger(attackTriggerName);
        }

        public void EndAttackAnimation()
        {
            OnEndAttackAnimation();
            ResetTrigger();
        }

        /// <summary>
        /// 플레이어 스크립트에서 공격할 때 사용할 함수
        /// </summary>
        /// <param name="value">공격 패턴 카운트</param>
        public void SetAttackAnimator(int value)
        {
            SetAnimatorParameter(AnimatorParameterType.Trigger, attackTriggerName);
            SetAnimatorParameter(AnimatorParameterType.Integer, attackPatternCountName, value);
        }

        /// <summary>
        /// 플레이어에서 호출할 Animator Parameter 설정할 함수
        /// </summary>
        public void SetAnimatorParameter(AnimatorParameterType parameterType, string parameterName,
            object value = null)
        {
            if (parameterType != AnimatorParameterType.Trigger && value == null)
                throw new NullReferenceException($"{nameof(SetAnimatorParameter)} Method : Parameter value is Null");

            switch (parameterType)
            {
                case AnimatorParameterType.Float:
                    animator.SetFloat(parameterName, (float)value);
                    break;
                case AnimatorParameterType.Integer:
                    animator.SetInteger(parameterName, (int)value);
                    break;
                case AnimatorParameterType.Bool:
                    animator.SetBool(parameterName, (bool)value);
                    break;
                case AnimatorParameterType.Trigger:
                    animator.SetTrigger(parameterName);
                    break;
            }
        }


        #endregion
    }
}