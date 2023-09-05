using UnityEngine;

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

    private IGuardianActions guardian;

    private void Start()
    {
        RunAndInteractionButton = FindObjectOfType<RunAndInteractionButton>();

        guardian = GameManager.Instance.GuardianData.Guardian;

        attackButton.OnMouseEnter += () =>
        {
            if (guardian.TryAttack(out float coolTime))
            {
                
            } 
        };

        skillButton.OnMouseEnter += () =>
        {
            if (guardian.TryUseSkill(out float coolTime))
            {
                
            }
        };

        if (guardian.HasAdditionalSkill)
        {
            additionalSkillButton.SetActive(true);
            additionalSkillButton.OnMouseEnter += () =>
            {
                if (guardian.TryUseAdditionalSKill(out float coolTime))
                {
                    
                }
            };
        }
    }
    
    
}