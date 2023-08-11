using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [Header("Move Buttons")]
    [SerializeField]
    private MovableButton moveUpButton;

    [SerializeField]
    private MovableButton moveDownButton;

    [SerializeField]
    private MovableButton moveLeftButton;

    [SerializeField]
    private MovableButton moveRightButton;

    [SerializeField]
    private Vector2 moveDiretion;
    
    public float sensitivity;

    private void FixedUpdate()
    {
        float h = 0f;
        float v = 0f;

        if (moveUpButton.IsPress)
            v = 1f;
        else if (moveDownButton.IsPress)
            v = -1f;

        if (moveLeftButton.IsPress)
            h = -1f;
        else if (moveRightButton.IsPress)
            h = 1f;

        moveDiretion = SmoothInput(h, v);
    }


    private Vector2 SmoothInput(float targetH, float targetV)
    {
        sensitivity = 3f;
        float deadZone = 0.001f;

        moveDiretion.x = Mathf.MoveTowards(moveDiretion.x,
            targetH, sensitivity * Time.deltaTime);

        moveDiretion.y = Mathf.MoveTowards(moveDiretion.y,
            targetV, sensitivity * Time.deltaTime);

        return new Vector2(
            (Mathf.Abs(moveDiretion.x) < deadZone) ? 0f : moveDiretion.x,
            (Mathf.Abs(moveDiretion.y) < deadZone) ? 0f : moveDiretion.y);
    }
}