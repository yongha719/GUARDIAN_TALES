using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class RunAndInteractionButton : MyButton, IRunAndInteractionButton
{
    private ButtonState buttonState = ButtonState.Run;

    public ButtonState ButtonState
    {
        get { return buttonState; }
        set
        {
            buttonState = value;
        }
    }

    [Tooltip("현재 Button State가 Run일때")]
    public event Action OnRunButtonDown = () => { };

    public event Action OnRunButtonUp = () => { };

    [Tooltip("현재 Button State가 Interaction일때")]
    public event Action OnInteractionButtonDown = () => { };

    public event Action OnInteractionButtonUp = () => { };

    
    protected override void Start()
    {
        base.Start();

        OnRunButtonDown += () =>
        {            
            GameManager.Instance.GuardianData.Speed.Value = 12;
        };
        
        OnRunButtonUp += () =>
        {            
            GameManager.Instance.GuardianData.Speed.Value = 5;
        };
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        
        if (ButtonState == ButtonState.Run)
        {
            OnRunButtonDown();
        }
        else if (ButtonState == ButtonState.Interaction)
        {
            OnInteractionButtonDown();
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        
        if (ButtonState == ButtonState.Run)
        {
            OnRunButtonUp();
        }
        else if (ButtonState == ButtonState.Interaction)
        {
            OnInteractionButtonUp();
        }
    }
}