using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Combatant
{
    public bool IsDead => characterInformation.IsDead;

    public void PerformAction(Controller.Direction input, Combatant target)
    {
        ActionInformation action = characterInformation.CharacterActions[input];
    }

    protected override void Die()
    {
        Debug.Log(characterInformation.CharacterName + " died");
    }
}
