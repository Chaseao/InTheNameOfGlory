using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyInformation : CharacterInformation
{
    [SerializeField] ActionInformation enemyAction;
    [SerializeField] int initialGold;

    public ActionInformation EnemyAction => enemyAction;
    public int InitialGold => initialGold;
}

