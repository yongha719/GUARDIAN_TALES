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
    IPointerMoveHandler, IPointerExitHandler
{
    [Header("버튼 상태")]
    public bool Interactable = true;

    public bool IsPress => isPointerDown || isPointerInside;

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
    

    /// <summary> 포인터가 버튼 안으로 들어왔을 때 이벤트 </summary>
    public event Action OnMouseEnter = () => { };

    /// <summary> 포인터가 버튼 밖으로 나왔을 때 </summary>
    public event Action OnMouseExit = () => { };

    /// <summary>  버튼이 눌리고 있을 때 이벤트 </summary>
    public event Action OnPressed = () => { };

    protected MyButtonState currentMyButtonState
    {
        get
        {
            if (Interactable == false)
                return MyButtonState.Disabled;
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

    protected void FixedUpdate()
    {
        if (IsPress)
        {
            OnPressed();
        }
    }

    public void Press()
    {
        isPointerDown = true;

        UpdateImageColorByButtonState();
    }

    public void Release()
    {
        isPointerDown = false;

        UpdateImageColorByButtonState();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Press();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        Release();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        isPointerInside = true;

        UpdateImageColorByButtonState();
    }

    public virtual void OnPointerMove(PointerEventData eventData) { }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        isPointerInside = false;

        OnMouseExit();
        UpdateImageColorByButtonState();
    }

    private void Reset()
    {
        Interactable = true;

        ColorBlock = new ColorBlock
        {
            normalColor = Color.white,
            highlightedColor = new Color(0.9608f, 1, 0.7333f),
            pressedColor = new Color(0.6588f, 0.6588f, 0.6588f),
            selectedColor = Color.white,
            disabledColor = new Color(0.3f, 0.3f, 0.3f)
        };
    }
}