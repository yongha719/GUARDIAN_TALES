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


/// <summary>
/// 
/// </summary>
public class MovableButton : MyButton
{
    [Space]
    public MoveDirType MoveDirType;

    protected override void Start()
    {
        base.Start();
        
        InputManager.Instance.PushMovableButton(this);
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

    public override void OnPointerMove(PointerEventData eventData)
    {
        base.OnPointerMove(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }

  
}