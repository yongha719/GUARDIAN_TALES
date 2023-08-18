using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonState
{
    Run,
    Interaction
}


public interface IRunAndInteractionButton
{
    ButtonState ButtonState { get; set; }

    event Action OnRunButtonDown;

    event Action OnRunButtonUp;

    event Action OnInteractionButtonDown;
    
    event Action OnInteractionButtonUp;
}