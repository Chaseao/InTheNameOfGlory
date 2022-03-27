using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CombatInformation
{
    Dictionary<Controller.InputTypes, Player> playerCombatants = new Dictionary<Controller.InputTypes, Player>();
    Dictionary<Controller.InputTypes, Enemy> enemyCombatants = new Dictionary<Controller.InputTypes, Enemy>();

    List<Player> playerOrder;
    List<Enemy> enemyOrder;

    public Dictionary<Controller.InputTypes, Player> PlayerCombatants => playerCombatants;
    public Dictionary<Controller.InputTypes, Enemy> EnemyCombatants => enemyCombatants;
    public List<Player> PlayerOrder => playerOrder;
    public List<Enemy> EnemyOrder => enemyOrder;

    public CombatInformation(Dictionary<Controller.InputTypes, Player> playerCombatants, Dictionary<Controller.InputTypes, Enemy> enemyCombatants)
    {
        this.playerCombatants = playerCombatants;
        this.playerOrder = new List<Player>(playerCombatants.Values);
        this.enemyCombatants = enemyCombatants;
        this.enemyOrder = new List<Enemy>(enemyCombatants.Values);
    }

    public void SwitchPlayers(Dictionary<Controller.InputTypes, Player> playerCombatants)
    {
        this.playerCombatants = playerCombatants;
        this.playerOrder = new List<Player>(playerCombatants.Values);
    }

    public void SwitchEnemies(Dictionary<Controller.InputTypes, Enemy> enemyCombatants)
    {
        this.enemyCombatants = enemyCombatants;
        this.enemyOrder = new List<Enemy>(enemyCombatants.Values);
    }
}