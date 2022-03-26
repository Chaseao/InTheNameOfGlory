using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class CombatManager : SerializedMonoBehaviour
{
    enum InputTypes
    {
        none,
        left,
        right
    }

    [SerializeField] Dictionary<Controller.Direction, Player> playerCombatants = new Dictionary<Controller.Direction, Player>();
    [SerializeField] Dictionary<Controller.Direction, Enemy> enemyCombatants = new Dictionary<Controller.Direction, Enemy>();

    List<Player> playerOrder;
    List<Enemy> enemyOrder;

    Controller.Direction lastInputDirection;
    InputTypes lastInputType = InputTypes.none;

    Combatant targetSelection;
    Controller.Direction actionSelection;

    private void Start()
    {
        Debug.Log("Starting the game...");
        playerOrder = new List<Player>(playerCombatants.Values);
        enemyOrder = new List<Enemy>(enemyCombatants.Values);
        
        StartCoroutine(StartNewCombat());
    }
    
    private IEnumerator StartNewCombat()
    {
        while(playerCombatants.Count > 0 && enemyCombatants.Count > 0)
        {
            yield return StartNewRound();
        }
    }

    private IEnumerator StartNewRound()
    {
        foreach (Player player in playerOrder)
        {
            Debug.Log("It is " + player.name + " turn");
            yield return GetActionInput();
            Debug.Log("Action Selected: " + actionSelection.ToString());
            yield return GetTargetInput();
            Debug.Log("Target Selected: " + targetSelection.name);

            player.TakeInput(actionSelection, targetSelection);
        }
        foreach (Enemy enemy in enemyOrder)
        {
            Debug.Log("It is " + enemy.name + " turn");

            Player randomTarget = playerOrder[UnityEngine.Random.Range(0, playerOrder.Count - 1)];
            Debug.Log("Target Selected: " + randomTarget.name);

            enemy.PerformRandomAction(randomTarget);
        }
    }

    private IEnumerator GetActionInput()
    {
        Controller.leftInput += GetLeftInput;

        lastInputType = InputTypes.none;
        yield return new WaitUntil(() => lastInputType.Equals(InputTypes.left));
        actionSelection = lastInputDirection;

        Controller.leftInput -= GetLeftInput;
    }

    private IEnumerator GetTargetInput()
    {
        bool inputWasValid = false;
        
        Controller.leftInput += GetLeftInput;
        Controller.rightInput += GetRightInput;

        while (!inputWasValid)
        {
            lastInputType = InputTypes.none;
            yield return new WaitUntil(() => !lastInputType.Equals(InputTypes.none));

            if (lastInputType.Equals(InputTypes.left) && playerCombatants.ContainsKey(lastInputDirection))
            {
                targetSelection = playerCombatants[lastInputDirection];
                inputWasValid = true;
            }
            else if (lastInputType.Equals(InputTypes.right) && enemyCombatants.ContainsKey(lastInputDirection))
            {
                targetSelection = enemyCombatants[lastInputDirection];
                inputWasValid = true;
            }
        }

        Controller.leftInput -= GetLeftInput;
        Controller.rightInput -= GetRightInput;
    }

    private void GetLeftInput(Controller.Direction input)
    {
        lastInputDirection = input;
        lastInputType = InputTypes.left;
    }

    public void GetRightInput(Controller.Direction input)
    {
        lastInputDirection = input;
        lastInputType = InputTypes.right;
    }
}
