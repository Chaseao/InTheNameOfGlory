using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New player", menuName = "Player")]
public class PlayerInformation : CharacterInformation
{
    [SerializeField] Dictionary<Controller.Direction, ActionInformation> characterActions = new Dictionary<Controller.Direction, ActionInformation>();
    public Dictionary<Controller.Direction, ActionInformation> CharacterActions => characterActions;
}

