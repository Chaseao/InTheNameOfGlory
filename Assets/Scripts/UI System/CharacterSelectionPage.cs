using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class CharacterSelectionPage : SerializedMonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerText;
    [SerializeField] Dictionary<PlayerInformation, TextMeshProUGUI> playerDescriptions = new Dictionary<PlayerInformation, TextMeshProUGUI>();

    public void Show()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        foreach(var description in playerDescriptions)
        {
            description.Value.text = description.Key.ActionsToString();
        }
    }

    public void Hide()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void UpdatePlayerNumber(int newPlayerNumber)
    {
        playerText.text = "Player " + newPlayerNumber;
    }
}