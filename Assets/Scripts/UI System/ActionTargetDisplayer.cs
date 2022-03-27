using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class ActionTargetDisplayer : SerializedMonoBehaviour
{
    [SerializeField] Dictionary<Controller.InputTypes, ActionTargetDisplay> leftDisplays;
    [SerializeField] Dictionary<Controller.InputTypes, ActionTargetDisplay> rightDisplays;

    public void DisplayAction(Player player)
    {
        Clear();

        foreach(var action in player.Actions)
        {
            leftDisplays[action.Key].DisplaySelectable(action.Value.ActionName);
        }
    }

    public void DisplayTargets(Dictionary<Controller.InputTypes, Player> players, Dictionary<Controller.InputTypes, Enemy> enemies)
    {
        foreach(var player in players)
        {
            leftDisplays[player.Key].DisplaySelectable(player.Value.CharacterInformation.CharacterName);
        }

        foreach(var enemy in enemies)
        {
            rightDisplays[enemy.Key].DisplaySelectable(enemy.Value.CharacterInformation.CharacterName);
        }
    }

    public void DisplayNonTargets(Dictionary<Controller.InputTypes, Player> players, Dictionary<Controller.InputTypes, Enemy> enemies)
    {
        foreach (var player in players)
        {
            leftDisplays[player.Key].DisplayNotSelectable(player.Value.CharacterInformation.CharacterName);
        }

        foreach (var enemy in enemies)
        {
            rightDisplays[enemy.Key].DisplayNotSelectable(enemy.Value.CharacterInformation.CharacterName);
        }
    }

    public void Clear()
    {
        foreach(var display in leftDisplays.Values)
        {
            display.Clear();
        }
        
        foreach(var display in rightDisplays.Values)
        {
            display.Clear();
        }
    }
}
