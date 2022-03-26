using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CombatManager : SerializedMonoBehaviour
{
    enum InputTypes
    {
        none,
        left,
        right
    }

    [SerializeField] Dictionary<Controller.Direction, Player> playerCombatants = new Dictionary<Controller.Direction, Player>();
    [SerializeField] Dictionary<Controller.Direction, Enemy> enemyCombatants = new Dictionary<Controller.Direction, Enemy>();
    [SerializeField] List<Enemy> enemyPool;
    [SerializeField] RoomDeck roomDeck;
    [SerializeField] int roomsToDraw;

    List<Player> playerOrder;
    List<Enemy> enemyOrder;
    List<Room> rooms;

    Controller.Direction lastInputDirection;
    InputTypes lastInputType = InputTypes.none;

    Combatant targetSelection;
    Controller.Direction actionSelection;

    private void Start()
    {
        Debug.Log("Starting the game...");
        playerOrder = new List<Player>(playerCombatants.Values);

        DrawRoomsFromDeck();
        
        StartCoroutine(StartNewCombat());
    }

    private void DrawRoomsFromDeck()
    {
        rooms = new List<Room>(roomDeck.Rooms);
        while(rooms.Count > roomsToDraw)
        {
            DrawRandomRoom();
        }
    }

    private Room DrawRandomRoom()
    {
        Room roomDrawn = rooms[Random.Range(0, rooms.Count)];
        rooms.Remove(roomDrawn);
        return roomDrawn;
    }


    private IEnumerator StartNewCombat()
    {
        InitializeRoom();

        while(playerCombatants.Count > 0 && enemyCombatants.Count > 0)
        {
            yield return StartNewRound();
        }
    }

    private void InitializeRoom()
    {
        enemyCombatants = new Dictionary<Controller.Direction, Enemy>();
        Room room = DrawRandomRoom();

        int counter = 0;

        foreach(var enemyInfo in room.Enemies)
        {
            enemyPool[counter].CreateEnemy(enemyInfo.Value);
            enemyCombatants.Add(enemyInfo.Key, enemyPool[counter]);
            counter++;
        }

        enemyOrder = new List<Enemy>(enemyCombatants.Values);
    }

    private IEnumerator StartNewRound()
    {
        foreach (Player player in playerOrder)
        {
            Debug.Log("It is " + player.name + " turn");
            yield return GetActionInput();
            Debug.Log("Action Selected: " + actionSelection.ToString());
            yield return GetTargetInput(player);
            Debug.Log("Target Selected: " + targetSelection.name);

            player.TakeInput(actionSelection, targetSelection);
        }
        foreach (Enemy enemy in enemyOrder)
        {
            Debug.Log("It is " + enemy.name + " turn");

            Player randomTarget = playerOrder[Random.Range(0, playerOrder.Count)];
            Debug.Log("Target Selected: " + randomTarget.name);

            enemy.PerformRandomAction(randomTarget);
        }
    }

    private IEnumerator GetActionInput()
    {
        Controller.leftInput += GetLeftInput;

        lastInputType = InputTypes.none;
        yield return new WaitUntil(() => lastInputType.Equals(InputTypes.left));
        actionSelection = lastInputDirection;

        Controller.leftInput -= GetLeftInput;
    }

    private IEnumerator GetTargetInput(Player player)
    {
        bool inputWasValid = false;
        
        Controller.leftInput += GetLeftInput;
        Controller.rightInput += GetRightInput;

        while (!inputWasValid)
        {
            lastInputType = InputTypes.none;
            yield return new WaitUntil(() => !lastInputType.Equals(InputTypes.none));

            if (lastInputType.Equals(InputTypes.left) && playerCombatants.ContainsKey(lastInputDirection))
            {
                targetSelection = playerCombatants[lastInputDirection];
                
                inputWasValid = player.IsValidTarget(actionSelection, targetSelection);
            }
            else if (lastInputType.Equals(InputTypes.right) && enemyCombatants.ContainsKey(lastInputDirection))
            {
                targetSelection = enemyCombatants[lastInputDirection];

                inputWasValid = player.IsValidTarget(actionSelection, targetSelection);
            }
        }

        Controller.leftInput -= GetLeftInput;
        Controller.rightInput -= GetRightInput;
    }

    private void GetLeftInput(Controller.Direction input)
    {
        lastInputDirection = input;
        lastInputType = InputTypes.left;
    }

    public void GetRightInput(Controller.Direction input)
    {
        lastInputDirection = input;
        lastInputType = InputTypes.right;
    }
}
