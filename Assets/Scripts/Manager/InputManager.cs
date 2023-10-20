using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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

    /// <summary>
    /// MoveDirType에서 Up, Down, Left, Right만 반환
    /// </summary>
    public MoveDirType FourWayMoveDirType
    {
        get
        {
            var absX = MathF.Abs(dir.x);
            var absY = MathF.Abs(dir.y);

            var max = Mathf.Max(absY, absX);

            var dirType = MoveDirType.Up;

            if (max == absX)
            {
                dirType = dir.x < 0 ? MoveDirType.Left : MoveDirType.Right;
            }
            else if (max == absY)
            {
                dirType = dir.y < 0 ? MoveDirType.Down : MoveDirType.Up;
            }

            return dirType;
        }
    }

    /// <summary>
    /// 플레이어가 바라보는 방향을 기준으로 회전값을 반환
    /// </summary>
    public Quaternion PlayerLookRotation => FourWayMoveDirType switch
    {
        MoveDirType.Left => Quaternion.Euler(0, 0, 90),
        MoveDirType.Right => Quaternion.Euler(0, 0, -90),
        MoveDirType.Up => Quaternion.identity,
        MoveDirType.Down => Quaternion.Euler(0, 0, 180)
    };

    private List<MovableButton> movableButtons = new(8);

    [Header("민감도"), Tooltip("민감도")]
    public float sensitivity = 3f;

    [Header("이동버튼 키 모음")]
    [SerializeField]
    private KeyCode UpButtonKey;
    [SerializeField]
    private KeyCode DownButtonKey;
    [SerializeField]
    private KeyCode LeftButtonKey;
    [SerializeField]
    private KeyCode RightButtonKey;

    [Space]
    [SerializeField, Tooltip("현재 눌린 이동 버튼")]
    private MovableButton curMovableButton;

    public MovableButton CurMovableButton
    {
        set
        {
            if (curMovableButton != null)
            {
                curMovableButton.IsSelected = false;
            }

            curMovableButton = value;
            curMovableButton.IsSelected = true;
        }
    }

    public MoveDirType PlayerMoveDirType { get; private set; }

    [Tooltip("눌린 이동 버튼이 있는지 체크")]
    public bool AnyButtonPressed;

    private Action InputMovableKeyAction = () => { };

    private Dictionary<MoveDirType, (Func<bool> pressKey, Func<bool> upKey)> dirTypeToInputKeyFuncs = new(8);

    private void Start()
    {
        dirTypeToInputKeyFuncs.Add(MoveDirType.Up, GetKeyFunc(UpButtonKey));
        dirTypeToInputKeyFuncs.Add(MoveDirType.Down, GetKeyFunc(DownButtonKey));
        dirTypeToInputKeyFuncs.Add(MoveDirType.Left, GetKeyFunc(LeftButtonKey));
        dirTypeToInputKeyFuncs.Add(MoveDirType.Right, GetKeyFunc(RightButtonKey));

        dirTypeToInputKeyFuncs.Add(MoveDirType.LeftUp, GetKeyFuncs(UpButtonKey, LeftButtonKey));
        dirTypeToInputKeyFuncs.Add(MoveDirType.LeftDown, GetKeyFuncs(DownButtonKey, LeftButtonKey));
        dirTypeToInputKeyFuncs.Add(MoveDirType.RightUp, GetKeyFuncs(UpButtonKey, RightButtonKey));
        dirTypeToInputKeyFuncs.Add(MoveDirType.RightDown, GetKeyFuncs(DownButtonKey, RightButtonKey));
    }

    private (Func<bool> pressKey, Func<bool> upKey) GetKeyFunc(KeyCode keyCode)
    {
        return (() => Input.GetKey(keyCode), () => Input.GetKeyUp(keyCode));
    }

    private (Func<bool> pressKey, Func<bool> upKey) GetKeyFuncs(KeyCode keyCode1, KeyCode keyCode2)
    {
        return (() => Input.GetKey(keyCode1) && Input.GetKey(keyCode2), () => Input.GetKeyUp(keyCode1) || Input.GetKeyUp(keyCode2));
    }

    public void PushMovableButton(MovableButton button)
    {
        print(button.name);

        InputMovableKeyAction += () =>
        {
            // 버튼의 이동 방향으로 Input 액션들을 가져옴
            var (pressKey, upKey) = dirTypeToInputKeyFuncs[button.MoveDirType];

            if (pressKey())
            {
                print("input key");

                button.Press();

                return;
            }

            if (upKey())
            {
                button.Release();
                AnyButtonPressed = false;

                return;
            }
        };

        button.OnPressed += () => OnPressMovableButtonEvent(button);

        button.OnMouseExit += () =>
        {
            AnyButtonPressed = false;
        };

        movableButtons.Add(button);
    }

    private void OnPressMovableButtonEvent(MovableButton button)
    {
        AnyButtonPressed = true;
        CurMovableButton = button;
        PlayerMoveDirType = button.MoveDirType;
    }

    private void Update()
    {
        InputMovableKeyAction();
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

        switch (PlayerMoveDirType)
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