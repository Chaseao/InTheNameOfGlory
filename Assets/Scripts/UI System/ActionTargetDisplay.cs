using UnityEngine;
using TMPro;
using UnityEngine.UI;

class ActionTargetDisplay : MonoBehaviour
{
    [SerializeField] Image displayIcon;
    [SerializeField] TextMeshProUGUI displayText;

    public void Display(string text)
    {
        displayIcon.enabled = true;
        displayText.text = text;
    }

    public void Clear()
    {
        displayIcon.enabled = false;
        displayText.text = "";
    }
}
