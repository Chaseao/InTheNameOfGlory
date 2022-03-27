using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Combatant
{
    [SerializeField] PlayerInformation playerInformation;

    public string ActionsToString => playerInformation.ActionsToString();
    public Dictionary<Controller.InputTypes, ActionInformation> Actions => playerInformation.CharacterActions;
    public override CharacterInformation CharacterInformation => playerInformation;

    public void SetCharacter(PlayerInformation newPlayer)
    {
        playerInformation = newPlayer;
    }

    public void SetMaxHealthMultiplier(float multiplier)
    {
        playerInformation.MaxHealthMultiplier = multiplier;
    }

    public void TakeInput(Controller.InputTypes input, Combatant target)
    {
        ActionInformation action = playerInformation.CharacterActions[input];

        PerformAction(target, action);
    }

    public bool IsValidTarget(Controller.InputTypes input, Combatant target)
    {
        bool isValid = true;
        ActionTypes actionType = playerInformation.CharacterActions[input].Type;

        switch (actionType)
        {
            case ActionTypes.Damage:
                isValid = target != this;
                break;
            case ActionTypes.Heal:
                isValid = target is Player;
                break;
            case ActionTypes.Steal:
                isValid = target != this;
                break;
        }

        return isValid;
    }

    protected override void Die()
    {
        Debug.Log(playerInformation.CharacterName + " died");
    }

    protected override void ResetGold()
    {
        CurrentGold = 0;
    }
}
