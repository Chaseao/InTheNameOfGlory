using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public enum Button
    {
        Up,
        Left,
        Right,
        Down
    }

    public delegate void LeftInput(Button direction);
    public static event LeftInput leftInput;
    public delegate void RightInput(Button direction);
    public static event RightInput rightInput;
    public delegate void Select();
    public static event Select select;

    public void OnLeftInput(InputAction.CallbackContext context)
    {
        Button direction = DirectionConverter(context.ReadValue<Vector2>());

        if (context.started)
        {
            leftInput?.Invoke(direction);
        }
    }

    public void OnRightInput(InputAction.CallbackContext context)
    {
        Button direction = DirectionConverter(context.ReadValue<Vector2>());

        if (context.started)
        {
            rightInput?.Invoke(direction);
        }
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            select?.Invoke();
        }
    }

    private Button DirectionConverter(Vector2 directionToConvert)
    {
        Button convertedDirection;

        float x = directionToConvert.x;
        float y = directionToConvert.y;

        if(x > 0)
        {
            convertedDirection = Button.Right;
        }
        else if (x < 0)
        {
            convertedDirection = Button.Left;
        }
        else if (y > 0)
        {
            convertedDirection = Button.Up;
        }
        else
        {
            convertedDirection = Button.Down;
        }

        return convertedDirection;
    }
}
