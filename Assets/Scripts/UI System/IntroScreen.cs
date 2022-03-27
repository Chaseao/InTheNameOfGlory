using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Linq;

public class IntroScreen : SerializedMonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] IntroPageDisplay titlePage;
    [SerializeField] IntroPageDisplay tutorialPage;
    [SerializeField] IntroPageDisplay creditsPage;
    [SerializeField] IntroPageDisplay playerSelectionPage;
    [SerializeField] CharacterSelectionPage characterSelectionPageDisplay;
    [SerializeField] CombatSystem combatSystem;
    [SerializeField] Dictionary<Controller.InputTypes, PlayerInformation> playerCharacterOptions;
    [SerializeField] Dictionary<Controller.InputTypes, Player> playerPool = new Dictionary<Controller.InputTypes, Player>();

    Dictionary<Controller.InputTypes, Player> playersActive = new Dictionary<Controller.InputTypes, Player>();
    private List<PlayerInformation> charactersTaken = new List<PlayerInformation>();

    private void Start()
    {
        OpenTitlePage();
    }

    private void HidePages()
    {
        titlePage.Hide();
        tutorialPage.Hide();
        creditsPage.Hide();
        playerSelectionPage.Hide();
        characterSelectionPageDisplay.Hide();
        combatSystem.gameObject.SetActive(false);
    }

    public void OpenTitlePage()
    {
        HidePages();
        background.SetActive(true);
        titlePage.Show();
        Controller.rightInput += NavigateTitlePage;
    }

    private void NavigateTitlePage(Controller.InputTypes inputType)
    {
        switch (inputType)
        {
            case Controller.InputTypes.Up:
                OpenCreditsPage();
                Controller.rightInput -= NavigateTitlePage;
                break;
            case Controller.InputTypes.Right:
                Application.Quit();
                break;
            case Controller.InputTypes.Down:
                OpenTutorialPage();
                Controller.rightInput -= NavigateTitlePage;
                break;
        }
    }

    private void OpenTutorialPage()
    {
        HidePages();
        tutorialPage.Show();
        Controller.rightInput += NavigateTutorialPage;
    }

    private void NavigateTutorialPage(Controller.InputTypes inputType)
    {
        switch (inputType)
        {
            case Controller.InputTypes.Down:
                OpenPlayerSelectionPage();
                Controller.rightInput -= NavigateTutorialPage;
                break;
        }
    }

    private void OpenCreditsPage()
    {
        HidePages();
        creditsPage.Show();
        Controller.rightInput += NavigateCreditsPage;
    }

    private void NavigateCreditsPage(Controller.InputTypes inputType)
    {
        switch (inputType)
        {
            case Controller.InputTypes.Down:
                OpenTitlePage();
                Controller.rightInput -= NavigateCreditsPage;
                break;
        }
    }

    private void OpenPlayerSelectionPage()
    {
        HidePages();
        playerSelectionPage.Show();
        Controller.rightInput += NavigatePlayerSelectionPage;
    }

    private void NavigatePlayerSelectionPage(Controller.InputTypes inputType)
    {
        switch (inputType)
        {
            case Controller.InputTypes.Up:
                StartCoroutine(OpenCharacterSelectionPage(2));
                Controller.rightInput -= NavigatePlayerSelectionPage;
                break;
            case Controller.InputTypes.Right:
                StartCoroutine(OpenCharacterSelectionPage(3));
                Controller.rightInput -= NavigatePlayerSelectionPage;
                break;
            case Controller.InputTypes.Down:
                StartCoroutine(OpenCharacterSelectionPage(4));
                Controller.rightInput -= NavigatePlayerSelectionPage;
                break;
        }
    }

    private IEnumerator OpenCharacterSelectionPage(int playerCount)
    {
        HidePages();
        characterSelectionPageDisplay.Show();
        Controller.rightInput += NavigateCharacterSelectionPage;

        charactersTaken.Clear();
        playersActive.Clear();
        
        while(charactersTaken.Count < playerCount)
        {
            int currentPlayer = charactersTaken.Count + 1;
            characterSelectionPageDisplay.UpdatePlayerNumber(currentPlayer);
            yield return new WaitUntil(() => currentPlayer == charactersTaken.Count);
        }

        Controller.rightInput -= NavigateCharacterSelectionPage;
        StartCombat();
    }

    private void NavigateCharacterSelectionPage(Controller.InputTypes inputType)
    {
        Controller.InputTypes nextPlayerPos = (Controller.InputTypes)charactersTaken.Count;
        PlayerInformation characterSelected = playerCharacterOptions[inputType];

        if (!charactersTaken.Contains(characterSelected))
        {
            playerPool[nextPlayerPos].SetCharacter(characterSelected);
            playersActive.Add(nextPlayerPos, playerPool[nextPlayerPos]);
            charactersTaken.Add(characterSelected);
        }
    }

    private void StartCombat()
    {
        HidePages();
        background.SetActive(false);
        combatSystem.gameObject.SetActive(true);
        combatSystem.StartGame(new CombatInformation(playersActive, new Dictionary<Controller.InputTypes, Enemy>()));
    }
}
