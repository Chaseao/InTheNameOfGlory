using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Left,
        Right,
        Down
    }

    public delegate void LeftInput(Direction direction);
    public event LeftInput leftInput;
    public delegate void RightInput(Direction direction);
    public event RightInput rightInput;

    public void OnLeftInput(InputAction.CallbackContext context)
    {
        Direction direction = DirectionConverter(context.ReadValue<Vector2>());

        if (context.started)
        {
            Debug.Log("Left " + direction.ToString());
            leftInput?.Invoke(direction);
        }
    }

    public void OnRightInput(InputAction.CallbackContext context)
    {
        Direction direction = DirectionConverter(context.ReadValue<Vector2>());

        if (context.started)
        {
            Debug.Log("Right " + direction.ToString());
            rightInput?.Invoke(direction);
        }
    }

    private Direction DirectionConverter(Vector2 directionToConvert)
    {
        Direction convertedDirection = Direction.Up;

        return convertedDirection;
    }
}
