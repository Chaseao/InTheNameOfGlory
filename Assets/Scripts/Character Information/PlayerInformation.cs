using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New player", menuName = "Player")]
public class PlayerInformation : CharacterInformation
{
    [SerializeField] Dictionary<Controller.Button, ActionInformation> characterActions = new Dictionary<Controller.Button, ActionInformation>();
    public Dictionary<Controller.Button, ActionInformation> CharacterActions => characterActions;
}

