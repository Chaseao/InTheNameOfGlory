using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public abstract class CharacterInformation : SerializedScriptableObject
{
    [SerializeField] string characterName;
    [SerializeField] int maxHealth;
    [SerializeField] RuntimeAnimatorController animation;

    public string CharacterName => characterName;
    public virtual int MaxHealth => maxHealth;
    public RuntimeAnimatorController Animation => animation;
}

