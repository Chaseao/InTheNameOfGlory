using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor.Animations;

public abstract class CharacterInformation : SerializedScriptableObject
{
    [SerializeField] string characterName;
    [SerializeField] int maxHealth;
    [SerializeField] AnimatorController animation;

    public string CharacterName => characterName;
    public virtual int MaxHealth => maxHealth;
    public AnimatorController Animation => animation;
}

