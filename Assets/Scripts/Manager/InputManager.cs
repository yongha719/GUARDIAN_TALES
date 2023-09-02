using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField, Tooltip("플레이어 이동 방향")]
    private Vector2 dir;

    public Vector2 Dir
    {
        get
        {
            if (AnyButtonPressed)
                return dir.normalized;

            return Vector2.zero;
        }
    }


    private List<MovableButton> movableButtons = new(8);

    [Header("민감도"), Tooltip("민감도")]
    public float sensitivity = 3f;

    [Space]
    [SerializeField, Tooltip("현재 눌린 이동 버튼")]
    private MovableButton curMovableButton;

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

    public MoveDirType PlayerMoveDirType { get; private set; }

    [Tooltip("눌린 이동 버튼이 있는지 체크")]
    public bool AnyButtonPressed;

    public void PushMovableButton(MovableButton button)
    {
        button.OnMouseEnter += () =>
        {
            AnyButtonPressed = true;
            CurMovableButton = button;
            PlayerMoveDirType = button.MoveDirType;
        };

        button.OnMouseExit += () =>
        {
            AnyButtonPressed = false;
        };

        movableButtons.Add(button);
    }

    // 버튼을 listen해서 dir을 수정하도록
    // 플레이어에서는 Dir을 참조해서 사용하도록
    private void FixedUpdate()
    {
        if (AnyButtonPressed == false)
        {
            dir = Vector2.zero;
            return;
        }

        CalculateMovementValues(out float h, out float v);

        dir = SmoothInput(h, v);
    }

    private Vector2 SmoothInput(float targetH, float targetV)
    {
        var deadZone = 0.001f;
        var smoothDelta = sensitivity * Time.deltaTime;

        dir.x = Mathf.MoveTowards(dir.x, targetH, smoothDelta);
        dir.y = Mathf.MoveTowards(dir.y, targetV, smoothDelta);

        return new Vector2(
            Mathf.Abs(dir.x) < deadZone ? 0f : dir.x,
            Mathf.Abs(dir.y) < deadZone ? 0f : dir.y);
    }
    
    public void CalculateMovementValues(out float h, out float v)
    {
        h = v = 0f;

        switch (curMovableButton.MoveDirType)
        {
            case MoveDirType.Up:
                v = 1f;
                break;
            case MoveDirType.RightUp:
                v = h = 1f;
                break;
            case MoveDirType.Right:
                h = 1f;
                break;
            case MoveDirType.RightDown:
                v = -1f;
                h = 1f;
                break;
            case MoveDirType.Down:
                v = -1f;
                break;
            case MoveDirType.LeftDown:
                v = h = -1f;
                break;
            case MoveDirType.Left:
                h = -1f;
                break;
            case MoveDirType.LeftUp:
                v = 1f;
                h = -1f;
                break;
        }
    }
}