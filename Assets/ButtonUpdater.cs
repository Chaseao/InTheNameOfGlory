using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ButtonUpdater : MonoBehaviour
{
    [SerializeField] Sprite controllerIcon;
    [SerializeField] Sprite keyboardIcon;
    [SerializeField] Image icon;

    private void OnEnable()
    {

        if (Gamepad.all.Count > 0)
        {
            icon.sprite = controllerIcon;
        }
        else
        {
            icon.sprite = keyboardIcon;
        }
    }
}
