using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public List<MovableButton> MovableButtons = new(8);

    [Tooltip("달리기 / 상호작용 버튼")]
    public Button RunAndInteractionButton;
    
    public static Vector2 moveDirection;

    [Tooltip("민감도")]
    public float sensitivity = 3f;

    public bool anyButtonPressed;
    public MovableButton MovableButton;

    private void Start()
    {
        foreach (var movableButton in MovableButtons)
            movableButton.PlayerUI = this;
    }

    private void FixedUpdate()
    {
        if (anyButtonPressed == false)
        {
            moveDirection = Vector2.zero;
            return;
        }

        MovableButton.CalculateMovementValues(out float h, out float v);

        moveDirection = SmoothInput(h, v);
    }

    private Vector2 SmoothInput(float targetH, float targetV)
    {
        var deadZone = 0.001f;
        var smoothDelta = sensitivity * Time.deltaTime;

        moveDirection.x = Mathf.MoveTowards(moveDirection.x, targetH, smoothDelta);
        moveDirection.y = Mathf.MoveTowards(moveDirection.y, targetV, smoothDelta);

        return new Vector2(
            Mathf.Abs(moveDirection.x) < deadZone ? 0f : moveDirection.x,
            Mathf.Abs(moveDirection.y) < deadZone ? 0f : moveDirection.y);
    }
}