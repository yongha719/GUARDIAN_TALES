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

public class MovableButton : MonoBehaviour,IPointerClickHandler, IPointerUpHandler, IPointerMoveHandler, IPointerExitHandler
{
    public MoveDirType MoveDirType;

    [field: SerializeField]
    public bool IsPress { get; private set; }

    public event Action OnPointEnterAction = () => { };

    public event Action OnClick = () => { };

    [SerializeField]
    private PlayerUIManager playerUIManager;


    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
        print($"{name} : Click");
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        print($"{name} : Move"); 
        playerUIManager.anyButtonPressed = IsPress = true;
        playerUIManager.MovableButton = this;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print($"{name} : Up");
        
        playerUIManager.anyButtonPressed = IsPress = false;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        playerUIManager.anyButtonPressed = IsPress = false;
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