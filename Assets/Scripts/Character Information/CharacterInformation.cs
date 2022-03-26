using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[CreateAssetMenu(fileName = "New character", menuName = "Character")]
public class CharacterInformation : SerializedScriptableObject
{
    [SerializeField] string characterName;
    [SerializeField] List<ActionInformation> characterAttacks = new List<ActionInformation>();
    [SerializeField] int maxHealth;
    [SerializeField, ReadOnly] int currentHealth;
    [SerializeField, ReadOnly] int currentCoins = 0;

    public string CharacterName => characterName;
    public List<ActionInformation> CharacterAttacks => new List<ActionInformation>(characterAttacks);

    public int CurrentHealth 
    { 
        get 
        {
            return currentHealth; 
        } 
        set 
        {
            currentHealth = Mathf.Max(0, currentHealth += value); 
        } 
    }

    public bool IsDead => currentHealth == 0;

    public int CurrentCoins => currentCoins;

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}

[System.Serializable]
public class ActionInformation
{
    public enum ActionType
    {
        Damage,
        Heal,
        Steal,
        Pass
    }

    [SerializeField] string actionName;
    [SerializeField] ActionType actionType;
    [SerializeField] bool groupAction;
    [SerializeField] int actionStat;
    [SerializeField, Range(0, 1)] float failChance;

    public string ActionName => actionName;
    public ActionType Type => actionType;
    public bool GroupAction => groupAction;
    public int ActionStat => actionStat;
    public float FailChance => failChance;
}
