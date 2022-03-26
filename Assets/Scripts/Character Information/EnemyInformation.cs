using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyInformation : CharacterInformation
{
    [SerializeField] ActionInformation enemyAction1;
    [SerializeField] ActionInformation enemyAction2;
    [SerializeField] int initialGold;

    public ActionInformation EnemyAction1 => enemyAction1;
    public ActionInformation EnemyAction2 => enemyAction2;
    public int InitialGold => initialGold;
}

