using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private bool pressed = false;

    public Vector2 Dir
    {
        get
        {
            if (pressed)
                return dir;
            return Vector2.zero;
        }
    }
    private Vector2 dir;

    private List<MovableButton> movableButtons = new List<MovableButton>();

    public void PushMovableButton(MovableButton button)
    {
        movableButtons.Add(button);
        print($"added button: {button.gameObject.name}");
    }

    // 버튼을 listen해서 dir을 수정하도록
    // 플레이어에서는 Dir을 참조해서 사용하도록
}
