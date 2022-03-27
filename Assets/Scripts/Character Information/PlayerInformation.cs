using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New player", menuName = "Player")]
public class PlayerInformation : CharacterInformation
{
    [SerializeField] Dictionary<Controller.InputTypes, ActionInformation> characterActions = new Dictionary<Controller.InputTypes, ActionInformation>();
    float maxHealthMultiplier = 0;

    public Dictionary<Controller.InputTypes, ActionInformation> CharacterActions => characterActions;
    public float MaxHealthMultiplier { get => maxHealthMultiplier; set => maxHealthMultiplier = Mathf.Max(0, value); }

    public override int MaxHealth => Mathf.RoundToInt(base.MaxHealth * maxHealthMultiplier);

    public string ActionsToString()
    {
        string actionsToString = CharacterName + "\n";

        for(int i = 0; i < 31; i++) 
        {
            actionsToString = string.Concat(actionsToString, "-");
        }

        actionsToString = string.Concat(actionsToString, "\n");

        foreach (ActionInformation action in characterActions.Values)
        {
            string actionValue = action.ActionStat.ToString();

            if (action.HasRange)
            {
                actionValue = action.ActionMin + " - " + action.ActionMax;
            }

            actionsToString = string.Concat(actionsToString, action.ActionName + ": " + action.Type.ToString() + " " + actionValue + "\n");
        }

        return actionsToString;
    }
}

