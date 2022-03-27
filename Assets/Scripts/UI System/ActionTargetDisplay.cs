using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

class ActionTargetDisplay : MonoBehaviour
{
    [SerializeField] Image displayIcon;
    [SerializeField] Sprite controllerIcon;
    [SerializeField] Sprite keyboardIcon;
    [SerializeField] TextMeshProUGUI displayText;
    [SerializeField] Image buttonBackground;
    [SerializeField] Color selectable;
    [SerializeField] Color notSelectable;

    public void DisplaySelectable(string text)
    {
        displayText.color = selectable;

        if(Gamepad.all.Count > 0)
        {
            displayIcon.sprite = controllerIcon;
        }
        else
        {
            displayIcon.sprite = keyboardIcon;
        }

        buttonBackground.enabled = true;
        displayIcon.enabled = true;
        displayText.text = text;
    }

    public void DisplayNotSelectable(string text)
    {
        DisplaySelectable(text);
        displayText.color = notSelectable;
    }

    public void Clear()
    {
        buttonBackground.enabled = false;
        displayIcon.enabled = false;
        displayText.text = "";
    }
}
