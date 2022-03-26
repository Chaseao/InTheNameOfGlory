using UnityEngine;
using Microsoft;

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

        if(enemyInformation.enemyAction.Type.equals(ActionTypes.Heal)){
            target=this;
        }
        if(enemyInformation.HasBonus){
            if(Random.Range(0,2)>=1){
                PerformAction(target,enemyInformation.EnemyAction1);
            }else{
                PerformAction(target,enemyInformation.BonusAction);
            }
        }else{
            PerformAction(target, enemyInformation.EnemyAction);
        }
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
