using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New character", menuName = "Character")]
public class CharacterInformation : ScriptableObject
{
    [SerializeField] string characterName;
    [SerializeField] List<ActionInformation> characterAttacks = new List<ActionInformation>();
    [SerializeField, ReadOnly] int currentCoins = 0;
}

public class ActionInformation : ScriptableObject
{

}
