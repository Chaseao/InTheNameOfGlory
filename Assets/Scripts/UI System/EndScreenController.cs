using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    [SerializeField] List<PlayerResultSlot> playerResultSlots;
    [SerializeField] Image selectIcon;
    [SerializeField] Sprite controllerIcon;
    [SerializeField] Sprite keyboardIcon;
    [SerializeField] WinnerDisplay winnerDisplay;
    [SerializeField] GameObject resultsPage;
    [SerializeField] IntroScreen introScreen;
    [SerializeField] CombatSystem combatSystem;

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

        if(UnityEngine.InputSystem.Gamepad.all.Count > 0)
        {
            selectIcon.sprite = controllerIcon;
        }
        else
        {
            selectIcon.sprite = keyboardIcon;
        }

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

        if (PlayersAllTied())
        {
            winnerDisplay.Display("None of You!");
        }
        else
        {
            winnerDisplay.Display(playersInGame.Last());
        }

        Controller.rightInput += HandleWinnerMenu;
    }

    private bool PlayersAllTied()
    {
        return playersInGame.First().CurrentGold == playersInGame.Last().CurrentGold;
    }

    private void HandleWinnerMenu(Controller.InputTypes input)
    {
        switch (input)
        {
            case Controller.InputTypes.Up:
                Controller.rightInput -= HandleWinnerMenu;
                introScreen.OpenTitlePage();
                gameObject.SetActive(false);
                break;
            case Controller.InputTypes.Left:
                break;
            case Controller.InputTypes.Right:
                Application.Quit();
                break;
            case Controller.InputTypes.Down:
                Controller.rightInput -= HandleWinnerMenu;
                combatSystem.RestartGame();
                gameObject.SetActive(false);
                break;
        }
    }
}
