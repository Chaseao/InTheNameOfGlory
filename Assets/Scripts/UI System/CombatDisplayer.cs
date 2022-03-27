using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CombatDisplayer : SerializedMonoBehaviour
{
    [SerializeField] Dictionary<Controller.Button, CombatantDisplayer> playerDisplays = new Dictionary<Controller.Button, CombatantDisplayer>();
    [SerializeField] Dictionary<Controller.Button, CombatantDisplayer> enemyDisplays = new Dictionary<Controller.Button, CombatantDisplayer>();

    public void DisplayCombatants(Dictionary<Controller.Button, Player> players, Dictionary<Controller.Button, Enemy> enemies)
    {
        foreach(var display in playerDisplays.Values)
        {
            display.Clear();
        }

        foreach(var player in players)
        {
            playerDisplays[player.Key].Display(player.Value);
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
