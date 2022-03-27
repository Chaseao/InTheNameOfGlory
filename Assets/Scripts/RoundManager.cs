using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class RoundManager : SerializedMonoBehaviour
{
    enum InputTypes
    {
        none,
        left,
        right
    }

    CombatInformation combatInformation;

    [SerializeField] ActionTargetDisplayer actionTargetDisplayer;
    [SerializeField] CombatDisplayer combatDisplayer;

    Controller.InputTypes lastInputDirection;
    InputTypes lastInputType = InputTypes.none;

    Combatant targetSelection;
    Controller.InputTypes actionSelection;

    public IEnumerator StartNewRound(CombatInformation combatInformation)
    {
        this.combatInformation = combatInformation;

        combatDisplayer.DisplayCombatants(this.combatInformation.PlayerCombatants, this.combatInformation.EnemyCombatants);

        foreach (Player player in this.combatInformation.PlayerOrder)
        {
            if (this.combatInformation.EnemyCombatants.Count > 0) {
                yield return HandlePlayerTurn(player);
            }
        }
        foreach (Enemy enemy in this.combatInformation.EnemyOrder)
        {
            if(this.combatInformation.PlayerCombatants.Count > 0)
            HandleEnemyTurn(enemy);
        }
    }

    private void HandleEnemyTurn(Enemy enemy)
    {
        Debug.Log("It is " + enemy.name + " turn");

        Player randomTarget = combatInformation.PlayerOrder[Random.Range(0, combatInformation.PlayerOrder.Count)];
        Debug.Log("Target Selected: " + randomTarget.name);

        enemy.PerformRandomAction(randomTarget);
        UpdateCombatants();
    }

    private IEnumerator HandlePlayerTurn(Player player)
    {
        Debug.Log("It is " + player.name + " turn");
        actionTargetDisplayer.DisplayAction(player);
        yield return GetActionInput();
        Debug.Log("Action Selected: " + actionSelection.ToString());

        DisplayTargets(player);
        yield return GetTargetInput(player);
        Debug.Log("Target Selected: " + targetSelection.name);

        player.TakeInput(actionSelection, targetSelection);
        UpdateCombatants(true);
    }

    private void DisplayTargets(Player player)
    {
        actionTargetDisplayer.Clear();

        var validPlayerTargets = combatInformation.PlayerCombatants
            .Where(combatant => player.IsValidTarget(actionSelection, combatant.Value))
            .ToDictionary(combatant => combatant.Key, combatant => combatant.Value);

        var validEnemyTargets = combatInformation.EnemyCombatants
            .Where(combatant => player.IsValidTarget(actionSelection, combatant.Value))
            .ToDictionary(combatant => combatant.Key, combatant => combatant.Value);

        actionTargetDisplayer.DisplayNonTargets(combatInformation.PlayerCombatants, combatInformation.EnemyCombatants);
        actionTargetDisplayer.DisplayTargets(validPlayerTargets, validEnemyTargets);
    }

    private void UpdateCombatants(bool hideGold = false)
    {
        RemoveDeadPlayers();
        RemoveDeadEnemies();
        combatDisplayer.DisplayCombatants(combatInformation.PlayerCombatants, combatInformation.EnemyCombatants, hideGold);
    }

    private void RemoveDeadPlayers()
    {
        var playersStillAlive = combatInformation.PlayerCombatants
            .Where(combatant => !combatant.Value.IsDead)
            .ToDictionary(combatant => combatant.Key, combatant => combatant.Value);

        combatInformation.SwitchPlayers(playersStillAlive);
    }

    private void RemoveDeadEnemies()
    {
        var enemiesStillAlive = combatInformation.EnemyCombatants
            .Where(combatant => !combatant.Value.IsDead)
            .ToDictionary(combatant => combatant.Key, combatant => combatant.Value);

        combatInformation.SwitchEnemies(enemiesStillAlive);
    }

    private IEnumerator GetActionInput()
    {
        Controller.leftInput += GetLeftInput;

        lastInputType = InputTypes.none;
        yield return new WaitUntil(() => lastInputType.Equals(InputTypes.left));
        actionSelection = lastInputDirection;

        Controller.leftInput -= GetLeftInput;
    }

    private IEnumerator GetTargetInput(Player player)
    {
        bool inputWasValid = false;
        
        Controller.leftInput += GetLeftInput;
        Controller.rightInput += GetRightInput;

        while (!inputWasValid)
        {
            lastInputType = InputTypes.none;
            yield return new WaitUntil(() => !lastInputType.Equals(InputTypes.none));

            if (lastInputType.Equals(InputTypes.left) && combatInformation.PlayerCombatants.ContainsKey(lastInputDirection))
            {
                targetSelection = combatInformation.PlayerCombatants[lastInputDirection];
                
                inputWasValid = player.IsValidTarget(actionSelection, targetSelection);
            }
            else if (lastInputType.Equals(InputTypes.right) && combatInformation.EnemyCombatants.ContainsKey(lastInputDirection))
            {
                targetSelection = combatInformation.EnemyCombatants[lastInputDirection];

                inputWasValid = player.IsValidTarget(actionSelection, targetSelection);
            }
        }

        Controller.leftInput -= GetLeftInput;
        Controller.rightInput -= GetRightInput;
    }

    private void GetLeftInput(Controller.InputTypes input)
    {
        lastInputDirection = input;
        lastInputType = InputTypes.left;
    }

    public void GetRightInput(Controller.InputTypes input)
    {
        lastInputDirection = input;
        lastInputType = InputTypes.right;
    }
}