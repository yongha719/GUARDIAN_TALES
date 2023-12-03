using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GUARDIANTALES
{
    public class PlayerUIManager : MonoBehaviour
    {
        [Space]
        [Tooltip("달리기 / 상호작용 버튼")]
        private IRunAndInteractionButton RunAndInteractionButton;

        [SerializeField]
        private MyButton attackButton;

        [SerializeField]
        private MyButton skillButton;

        [SerializeField]
        private MyButton additionalSkillButton;

        private Guardian guardian;

        private void Start()
        {
            RunAndInteractionButton = FindObjectOfType<RunAndInteractionButton>();

            guardian = GameManager.Instance.GuardianData.Guardian;

            attackButton.OnPressed += () =>
            {
                guardian.TryAttack();
            };

            skillButton.OnPressed += () =>
            {
                if (guardian.TryUseSkill(out float coolTime))
                {
                    print("Use Skill!");
                }
            };

            additionalSkillButton.SetActive(guardian.HasAdditionalSkill);
            additionalSkillButton.OnPressed += () =>
            {
                if (guardian.TryUseAdditionalSKill(out float coolTime))
                {
                    print("Use Additional Skill!");
                }
            };
        }
    }
}