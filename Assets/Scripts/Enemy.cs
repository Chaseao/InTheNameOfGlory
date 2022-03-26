using UnityEngine;

public class Enemy : Combatant
{
    public void PerformRandomAction(Combatant target)
    {
        
    }

    protected override void Die()
    {
        Debug.Log(characterInformation.CharacterName + " died");
    }
}
