using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public enum InputTypes
    {
        Up,
        Left,
        Right,
        Down
    }

    public delegate void LeftInput(InputTypes direction);
    public static event LeftInput leftInput;
    public delegate void RightInput(InputTypes direction);
    public static event RightInput rightInput;
    public delegate void Select();
    public static event Select select;

    public void OnLeftInput(InputAction.CallbackContext context)
    {
        InputTypes direction = DirectionConverter(context.ReadValue<Vector2>());

        if (context.started)
        {
            leftInput?.Invoke(direction);
        }
    }

    public void OnRightInput(InputAction.CallbackContext context)
    {
        InputTypes direction = DirectionConverter(context.ReadValue<Vector2>());

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

    private InputTypes DirectionConverter(Vector2 directionToConvert)
    {
        InputTypes convertedDirection;

        float x = directionToConvert.x;
        float y = directionToConvert.y;

        if(x > 0)
        {
            convertedDirection = InputTypes.Right;
        }
        else if (x < 0)
        {
            convertedDirection = InputTypes.Left;
        }
        else if (y > 0)
        {
            convertedDirection = InputTypes.Up;
        }
        else
        {
            convertedDirection = InputTypes.Down;
        }

        return convertedDirection;
    }
}
