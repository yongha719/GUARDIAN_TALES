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

    private Guardian guardian;

    private void Start()
    {
        RunAndInteractionButton = FindObjectOfType<RunAndInteractionButton>();

        guardian = GameManager.Instance.GuardianData.Guardian;

        attackButton.OnMouseEnter += () => guardian.AttackCoolDown.IsCooldownFinished();
        skillButton.OnMouseEnter += guardian.TryUseSkill;
        
        if (guardian.HasAdditionalSkill)
        {
            additionalSkillButton.SetActive(true);
            additionalSkillButton.OnMouseEnter += () => guardian.AdditionalSkillCoolDown.IsCooldownFinished();
        }
    }
}