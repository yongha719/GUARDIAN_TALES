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

    private Player player;

    private void Start()
    {
        RunAndInteractionButton = FindObjectOfType<RunAndInteractionButton>();

        player = FindObjectOfType<Player>();

        attackButton.OnMouseEnter += () => player.AttackCoolDown.IsCooldownFinished();
        skillButton.OnMouseEnter += player.TryUseSkill;

        if (player.HasAdditionalSkill)
        {
            additionalSkillButton.SetActive(true);
            additionalSkillButton.OnMouseEnter += () => player.AdditionalSkillCoolDown.IsCooldownFinished();
        }
    }
}