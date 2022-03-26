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
        
        ActionInformation enemyActionType;
        if(enemyInformation.HasBonus){
            if(Random.Range(0,2)>=1){
               enemyActionType = enemyInformation.EnemyAction;
            }else{
                enemyActionType = enemyInformation.BonusAction;
            }
        }else{
               enemyActionType = enemyInformation.EnemyAction;
        }

        if(enemyActionType.Type.Equals(ActionTypes.Heal)) target =this;


        PerformAction(target,enemyActionType);
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
