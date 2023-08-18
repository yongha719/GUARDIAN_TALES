using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [Space]
    [Tooltip("달리기 / 상호작용 버튼")]
    public IRunAndInteractionButton RunAndInteractionButton;

    private void Start()
    {
        RunAndInteractionButton = FindObjectOfType<RunAndInteractionButton>();
    }
}