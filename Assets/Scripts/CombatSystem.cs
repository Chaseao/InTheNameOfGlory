using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CombatSystem : SerializedMonoBehaviour
{
    [SerializeField] List<Enemy> enemyPool;
    [SerializeField] RoomDeck minionDeck;
    [SerializeField] RoomDeck bossDeck;
    [SerializeField] int roomsToDraw;
    [SerializeField] RoundManager roundManager;
    [SerializeField] EndScreenController endScreen;

    CombatInformation initialInformation;
    CombatInformation combatInformation;
    List<Room> rooms;

    public void RestartGame()
    {
        StartGame(initialInformation);
    }

    public void StartGame(CombatInformation combatInformation)
    {
        Debug.Log("Starting the game...");

        initialInformation = combatInformation;

        this.combatInformation = new CombatInformation(combatInformation);

        foreach (Player player in combatInformation.PlayerOrder)
        {
            player.ResetCharacter();
        }

        DrawRoomsFromDecks();

        StartCoroutine(StartDungeon(new List<Player>(initialInformation.PlayerOrder)));
    }

    private void DrawRoomsFromDecks()
    {
        rooms = new List<Room>(minionDeck.Rooms);

        while (rooms.Count > roomsToDraw)
        {
            DrawRandomRoom();
        }

        rooms.Add(DrawRandomBossRoom());
    }

    private Room DrawRandomRoom()
    {
        Room roomDrawn = rooms[Random.Range(0, rooms.Count)];
        rooms.Remove(roomDrawn);
        return roomDrawn;
    }

    private Room DrawRandomBossRoom()
    {
        Room roomDrawn = bossDeck.Rooms[Random.Range(0, bossDeck.Rooms.Count)];
        return roomDrawn;
    }

    private IEnumerator StartDungeon(List<Player> playersInGame)
    {
        roundManager.gameObject.SetActive(true);

        while (rooms.Count > 0)
        {
            yield return StartNewCombat();
        }

        roundManager.gameObject.SetActive(false);

        endScreen.gameObject.SetActive(true);
        endScreen.DisplayEnding(playersInGame);
    }

    private IEnumerator StartNewCombat()
    {
        InitializeRoom();

        while (combatInformation.PlayerCombatants.Count > 0 && combatInformation.EnemyCombatants.Count > 0)
        {
            yield return roundManager.StartNewRound(combatInformation);
        }
    }

    private void InitializeRoom()
    {
        var newCombatants = new Dictionary<Controller.InputTypes, Enemy>();
        Room room = rooms[0];
        rooms.RemoveAt(0);

        int counter = 0;

        foreach (var enemyInfo in room.Enemies)
        {
            enemyPool[counter].CreateEnemy(enemyInfo.Value);
            newCombatants.Add(enemyInfo.Key, enemyPool[counter]);
            counter++;
        }

        combatInformation.SwitchEnemies(newCombatants);
    }
}
