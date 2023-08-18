using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

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

public class MovableButton : MyButton
{
    [Space]
    public MoveDirType MoveDirType;

    [SerializeField]
    private PlayerUIManager playerUIManager;


    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        print($"{name} : Click");
    }
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        print($"{name} : Up");

        playerUIManager.AnyButtonPressed = false;
    }

    public override void OnPointerMove(PointerEventData eventData)
    {
        base.OnPointerMove(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        print("OnPointerExit");
        // playerUIManager.AnyButtonPressed = IsPress = false;
    }

    public void CalculateMovementValues(out float h, out float v)
    {
        h = v = 0f;

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
                h = -1f;
                break;
            case MoveDirType.LeftUp:
                v = 1f;
                h = -1f;
                break;
        }
    }
}