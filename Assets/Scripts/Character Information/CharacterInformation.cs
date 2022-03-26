using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[CreateAssetMenu(fileName = "New character", menuName = "Character")]
public class CharacterInformation : SerializedScriptableObject
{
    [SerializeField] string characterName;
    [SerializeField] Dictionary<Controller.Direction, ActionInformation> characterActions = new Dictionary<Controller.Direction, ActionInformation>();
    [SerializeField] int maxHealth;
    [SerializeField, ReadOnly] int currentHealth;
    [SerializeField, ReadOnly] int currentGold = 0;

    public string CharacterName => characterName;
    public Dictionary<Controller.Direction, ActionInformation> CharacterActions => characterActions;

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

    public int CurrentGold
    {
        get
        {
            return currentGold;
        }
        set
        {
            currentGold = Mathf.Max(0, currentGold += value);
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
