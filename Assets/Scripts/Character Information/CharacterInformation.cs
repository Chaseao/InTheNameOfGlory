using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public abstract class CharacterInformation : SerializedScriptableObject
{
    [SerializeField] string characterName;
    [SerializeField] int maxHealth;

    public string CharacterName => characterName;
    public int MaxHealth => maxHealth;
}

