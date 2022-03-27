using TMPro;
using UnityEngine;

public class WinnerDisplay : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] TextMeshProUGUI winner;

    public void Display(Player player)
    {
        background.SetActive(true);
        winner.text = player.CharacterInformation.CharacterName;
    }

    public void Clear()
    {
        background.SetActive(false);
        winner.text = "";
    }
}