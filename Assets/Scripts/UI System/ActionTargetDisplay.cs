using UnityEngine;
using TMPro;
using UnityEngine.UI;

class ActionTargetDisplay : MonoBehaviour
{
    [SerializeField] Image displayIcon;
    [SerializeField] TextMeshProUGUI displayText;
    [SerializeField] Color selectable;
    [SerializeField] Color notSelectable;

    public void DisplaySelectable(string text)
    {
        displayText.color = selectable;

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
        displayIcon.enabled = false;
        displayText.text = "";
    }
}
