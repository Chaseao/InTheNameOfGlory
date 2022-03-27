using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    [SerializeField] List<PlayerResultSlot> playerResultSlots;
    [SerializeField] Image selectIcon;
    [SerializeField] WinnerDisplay winnerDisplay;
    [SerializeField] GameObject resultsPage;

    List<Player> playersInGame;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void DisplayBackground()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void DisplayEnding(List<Player> players)
    {
        selectIcon.enabled = false;
        DisplayBackground();

        playersInGame = players;
        playersInGame.Sort((playerOne, playerTwo) => playerOne.CurrentGold.CompareTo(playerTwo.CurrentGold));

        winnerDisplay.Clear();
        resultsPage.SetActive(true);
        StartCoroutine(DisplayPlayers());
    }

    private IEnumerator DisplayPlayers()
    {
        foreach(PlayerResultSlot slot in playerResultSlots)
        {
            slot.Clear();
        }

        int counter = 0;
        foreach (Player player in playersInGame)
        {
            yield return playerResultSlots[counter].DisplayPlayer(player);
            yield return new WaitForSeconds(0.5f);
            counter++;
        }

        selectIcon.enabled = true;
        Controller.select += DisplayWinner;
    }

    private void DisplayWinner()
    {
        Controller.select -= DisplayWinner;

        resultsPage.SetActive(false);

        winnerDisplay.Display(playersInGame.Last());

        Controller.rightInput += HandleWinnerMenu;
    }

    private void HandleWinnerMenu(Controller.Button input)
    {
        switch (input)
        {
            case Controller.Button.Up:
                break;
            case Controller.Button.Left:
                break;
            case Controller.Button.Right:
                Controller.rightInput -= HandleWinnerMenu;
                FindObjectOfType<CombatManager>().StartGame();
                gameObject.SetActive(false);
                break;
            case Controller.Button.Down:
                Controller.rightInput -= HandleWinnerMenu;
                Application.Quit();
                break;
        }
    }
}
