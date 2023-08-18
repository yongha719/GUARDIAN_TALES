using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum MyButtonState
{
    Normal,
    Highlighted,
    Pressed,
    Selected,
    Disabled
}

public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler,
    IPointerMoveHandler,
    IPointerExitHandler
{
    [Header("버튼 상태")]
    public bool Interactable = true;

    public bool IsPress => isPointerDown;


    [SerializeField]
    private bool isSelected;

    public bool IsSelected
    {
        get => isSelected;
        set
        {
            isSelected = value;
            UpdateImageColorByButtonState();
        }
    }

    [SerializeField]
    protected bool isPointerInside;

    [SerializeField]
    protected bool isPointerDown;

    [Header("Color")]
    public ColorBlock ColorBlock;

    public event Action OnMouseDown = () => { };
    public event Action OnMouseUp = () => { };


    protected MyButtonState currentMyButtonState
    {
        get
        {
            if (Interactable == false)
                return MyButtonState.Disabled;
            if (isPointerDown)
                return MyButtonState.Pressed;
            if (IsSelected)
                return MyButtonState.Selected;
            if (isPointerInside)
                return MyButtonState.Highlighted;
            return MyButtonState.Normal;
        }
    }

    protected Image image;

    protected virtual void Start()
    {
        image = GetComponent<Image>();

        UpdateImageColorByButtonState();
    }

    private void UpdateImageColorByButtonState()
    {
        switch (currentMyButtonState)
        {
            case MyButtonState.Normal:
                image.color = ColorBlock.normalColor;
                break;
            case MyButtonState.Highlighted:
                image.color = ColorBlock.highlightedColor;
                break;
            case MyButtonState.Pressed:
                image.color = ColorBlock.pressedColor;
                break;
            case MyButtonState.Selected:
                image.color = ColorBlock.selectedColor;
                break;
            case MyButtonState.Disabled:
                image.color = ColorBlock.disabledColor;
                break;
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;

        OnMouseDown();
        UpdateImageColorByButtonState();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;

        OnMouseUp();
        UpdateImageColorByButtonState();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        isPointerInside = true;
        UpdateImageColorByButtonState();
    }

    public virtual void OnPointerMove(PointerEventData eventData)
    {
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        isPointerInside = false;
        UpdateImageColorByButtonState();
    }
}