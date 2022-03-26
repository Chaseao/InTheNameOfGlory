using UnityEngine;

public class Enemy : Combatant
{
    [SerializeField] EnemyInformation enemyInformation;

    public override CharacterInformation CharacterInformation => enemyInformation;

    public void CreateEnemy(EnemyInformation enemyInformation)
    {
        this.enemyInformation = enemyInformation;
        ResetCharacter();
    }

    public void PerformRandomAction(Combatant target)
    {
        PerformAction(target, enemyInformation.EnemyAction);
    }

    protected override void Die()
    {
        Debug.Log(enemyInformation.CharacterName + " died");
    }

    protected override void ResetGold()
    {
        CurrentGold = enemyInformation.InitialGold;
    }
}
