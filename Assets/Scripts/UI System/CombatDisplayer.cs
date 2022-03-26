using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CombatDisplayer : SerializedMonoBehaviour
{
    [SerializeField] Dictionary<Controller.Direction, CombatantDisplayer> playerDisplays = new Dictionary<Controller.Direction, CombatantDisplayer>();
    [SerializeField] Dictionary<Controller.Direction, CombatantDisplayer> enemyDisplays = new Dictionary<Controller.Direction, CombatantDisplayer>();

    public void DisplayCombatants(Dictionary<Controller.Direction, Player> players, Dictionary<Controller.Direction, Enemy> enemies)
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
