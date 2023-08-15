using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [Header("이동 버튼")]
    public List<MovableButton> MovableButtons = new(8);

    [SerializeField,Tooltip("현재 눌린 이동 버튼")]
    private MovableButton curMovableButton;
    
    public bool anyButtonPressed;
    
    [Space]
    [Tooltip("달리기 / 상호작용 버튼")]
    public Button RunAndInteractionButton;

    public static Vector2 MoveDirection;

    [Header("민감도"), Tooltip("민감도")]
    public float sensitivity = 3f;



    public MovableButton MovableButton
    {
        set => curMovableButton = value;
    }


    private void FixedUpdate()
    {
        if (anyButtonPressed == false)
        {
            MoveDirection = Vector2.zero;
            return;
        }

        curMovableButton.CalculateMovementValues(out float h, out float v);

        MoveDirection = SmoothInput(h, v);
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