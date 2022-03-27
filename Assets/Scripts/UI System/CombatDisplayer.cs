using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CombatDisplayer : SerializedMonoBehaviour
{
    [SerializeField] Dictionary<Controller.InputTypes, CombatantDisplayer> playerDisplays = new Dictionary<Controller.InputTypes, CombatantDisplayer>();
    [SerializeField] Dictionary<Controller.InputTypes, CombatantDisplayer> enemyDisplays = new Dictionary<Controller.InputTypes, CombatantDisplayer>();

    public void DisplayCombatants(Dictionary<Controller.InputTypes, Player> players, Dictionary<Controller.InputTypes, Enemy> enemies, bool hideGold = false)
    {
        foreach(var display in playerDisplays.Values)
        {
            display.Clear();
        }

        foreach(var player in players)
        {
            playerDisplays[player.Key].Display(player.Value, hideGold && !player.Value.IsDead);
        }

        foreach(var display in enemyDisplays.Values)
        {
            display.Clear();
        }

        foreach(var enemy in enemies)
        {
            enemyDisplays[enemy.Key].Display(enemy.Value);
        }
    }
}
