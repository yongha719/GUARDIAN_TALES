using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public enum MoveDirType
{
    None = 0,
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

    protected override void Start()
    {
        base.Start();
        
        InputManager.Instance.PushMovableButton(this);
    }
}