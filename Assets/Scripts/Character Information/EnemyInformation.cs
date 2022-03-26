using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyInformation : CharacterInformation
{
    [SerializeField] bool hasBonus;
    [ShowIf("@hasBonus == true")]
    [SerializeField] ActionInformation bonusAction;
    [SerializeField] ActionInformation enemyAction;
    [SerializeField] int initialGold;

    public bool HasBonus => hasBonus;

    public ActionInformation EnemyAction => enemyAction;
    public int InitialGold => initialGold;
}

