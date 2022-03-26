using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Room", menuName = "Room")]
public class Room : SerializedScriptableObject
{
    [SerializeField] Dictionary<Controller.Direction, EnemyInformation> enemies = new Dictionary<Controller.Direction, EnemyInformation>();

    public Dictionary<Controller.Direction, EnemyInformation> Enemies => enemies;
 }
