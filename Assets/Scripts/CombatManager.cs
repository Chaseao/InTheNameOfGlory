using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class CombatManager : SerializedMonoBehaviour
{
    [SerializeField] Dictionary<Controller.Direction, Player> playerCombatants;
    [SerializeField] Dictionary<Controller.Direction, Combatant> enemyCombatants;

    List<Player> playerOrder;
    List<Combatant> enemyOrder;

    bool validInputRecieved;
    Controller.Direction lastInput;
    Combatant targetSelection;
    Controller.Direction actionSelection;

    private void Start()
    {
        Debug.Log("Starting the game...");
        playerOrder = new List<Player>(playerCombatants.Values);
        enemyOrder = new List<Combatant>(enemyCombatants.Values);
        
        StartCoroutine(StartNewCombat());
    }
    
    private IEnumerator StartNewCombat()
    {
        while(playerCombatants.Count > 0 && enemyCombatants.Count > 0)
        {
            StartCoroutine(StartNewRound());
        }

        yield return null;
    }

    private IEnumerator StartNewRound()
    {
        foreach (Player player in playerOrder)
        {
            validInputRecieved = false;
            GetActionInput();
            yield return new WaitUntil(() => validInputRecieved);

            validInputRecieved = false;
            StartCoroutine(GetTargetInput());
            yield return new WaitUntil(() => validInputRecieved);
        }
    }

    private void GetActionInput()
    {
        bool inputWasValid = false;
        bool inputRecieved = false;
        Action notifyThatInputRecieved = () => inputRecieved = true;

        while (!inputWasValid)
        {
            Controller.leftInput += GetInput;
            Controller.rightInput += GetInput;
            while (!inputRecieved)
            {

            }
        }
    }

    private IEnumerator GetTargetInput()
    {
        throw new NotImplementedException();
    }

    public void GetInput(Controller.Direction input)
    {
        lastInput = input;
    }
}
