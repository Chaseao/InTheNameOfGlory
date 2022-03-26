using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Combatant
{
    [SerializeField] PlayerInformation playerInformation;
    protected override CharacterInformation CharacterInformation => playerInformation;

    public void TakeInput(Controller.Direction input, Combatant target)
    {
        ActionInformation action = playerInformation.CharacterActions[input];

        PerformAction(target, action);
    }

    public bool IsValidTarget(Controller.Direction input, Combatant target)
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
