using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Room", menuName = "Room")]
public class Room : SerializedScriptableObject
{
    [SerializeField] Dictionary<Controller.Button, EnemyInformation> enemies = new Dictionary<Controller.Button, EnemyInformation>();

    public Dictionary<Controller.Button, EnemyInformation> Enemies => enemies;
 }
