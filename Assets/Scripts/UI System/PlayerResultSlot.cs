using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerResultSlot : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI playerCoins;

    public IEnumerator DisplayPlayer(Player player)
    {
        background.enabled = true;
        playerName.text = player.CharacterInformation.CharacterName;

        int coinsDisplayed = 0;
        playerCoins.text = "Final Coins: " + coinsDisplayed;

        while (coinsDisplayed < player.CurrentGold)
        {
            coinsDisplayed++;
            playerCoins.text = "Final Coins: " + coinsDisplayed;
            yield return new WaitForSeconds(2.0f / player.CurrentGold);
        }
    }

    public void Clear()
    {
        background.enabled = false;
        playerName.text = "";
        playerCoins.text = "";
    }
}
