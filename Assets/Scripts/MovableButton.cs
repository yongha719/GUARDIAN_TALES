using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum MoveDirType
{
    Up,
    RightUp,
    Right,
    RightDown,
    Down,
    LeftDown,
    Left,
    LeftUp
}

public class MovableButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MoveDirType MoveDirType;

    public bool IsPress { get; private set; }

    public event Action OnPointEnterAction = () => { };

    [SerializeField]
    private Vector2 moveDirection;

    [Tooltip("민감도")]
    public float sensitivity = 3f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        print(MoveDirType);


        IsPress = true;

        OnPointEnterAction();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        IsPress = false;
    }

    private void FixedUpdate()
    {
        if (IsPress == false)
            return;

        var h = 0f;
        var v = 0f;

        switch (MoveDirType)
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
                v = -1f;
                break;
            case MoveDirType.LeftUp:
                v = 1f;
                h = -1f;
                break;
        }

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