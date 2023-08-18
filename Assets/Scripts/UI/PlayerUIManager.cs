using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [Header("이동 버튼")]
    public List<MovableButton> MovableButtons = new(8);

    [SerializeField, Tooltip("현재 눌린 이동 버튼")]
    private MovableButton curMovableButton;

    [Tooltip("눌린 이동 버튼이 있는지 체크")]
    public bool AnyButtonPressed;

    [Header("민감도"), Tooltip("민감도")]
    public float sensitivity = 3f;

    private Vector2 MoveDirection;

    private PlayerData playerData;

    [Space]
    [Tooltip("달리기 / 상호작용 버튼")]
    public IRunAndInteractionButton RunAndInteractionButton;


    public MovableButton CurMovableButton
    {
        set
        {
            if (curMovableButton != null)
            {
                print(curMovableButton.name + ": false");
                curMovableButton.IsSelected = false;
            }

            curMovableButton = value;
            print(curMovableButton.name + ": true");
            curMovableButton.IsSelected = true;
        }
    }

    private void Start()
    {
        RunAndInteractionButton = FindObjectOfType<RunAndInteractionButton>();

        playerData = GameManager.Instance.PlayerData;

        foreach (var movableButton in MovableButtons)
        {
            var buttonCopy = movableButton; // 클로저 변수 복사
            buttonCopy.OnMouseDown += () =>
            {
                print(buttonCopy.name);
                CurMovableButton = buttonCopy;
                AnyButtonPressed = true;
            };

            buttonCopy.OnMouseUp += () =>
            {
                AnyButtonPressed = false;
            };
        }
    }

    private void FixedUpdate()
    {
        if (AnyButtonPressed == false)
        {
            MoveDirection = Vector2.zero;
            return;
        }

        curMovableButton.CalculateMovementValues(out float h, out float v);

        MoveDirection = SmoothInput(h, v);

        playerData.MoveDirection = MoveDirection;
    }

    private Vector2 SmoothInput(float targetH, float targetV)
    {
        var deadZone = 0.001f;
        var smoothDelta = sensitivity * Time.deltaTime;

        MoveDirection.x = Mathf.MoveTowards(MoveDirection.x, targetH, smoothDelta);
        MoveDirection.y = Mathf.MoveTowards(MoveDirection.y, targetV, smoothDelta);

        return new Vector2(
            Mathf.Abs(MoveDirection.x) < deadZone ? 0f : MoveDirection.x,
            Mathf.Abs(MoveDirection.y) < deadZone ? 0f : MoveDirection.y);
    }
}