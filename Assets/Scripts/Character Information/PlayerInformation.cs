using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New player", menuName = "Player")]
public class PlayerInformation : CharacterInformation
{
    [SerializeField] Dictionary<Controller.InputTypes, ActionInformation> characterActions = new Dictionary<Controller.InputTypes, ActionInformation>();
    public Dictionary<Controller.InputTypes, ActionInformation> CharacterActions => characterActions;

    public string ActionsToString()
    {
        string actionsToString = CharacterName + "\n";

        for(int i = 0; i < 31; i++) 
        {
            actionsToString.Concat("-");
        }

        actionsToString.Concat("\n");

        foreach(ActionInformation action in characterActions.Values)
        {
            actionsToString.Concat(action.ActionName + ": " + action.Type.ToString() + " " + action.ActionStat);
        }

        return actionsToString;
    }
}

