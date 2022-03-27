using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Room", menuName = "Room")]
public class Room : SerializedScriptableObject
{
    [SerializeField] Dictionary<Controller.InputTypes, EnemyInformation> enemies = new Dictionary<Controller.InputTypes, EnemyInformation>();

    public Dictionary<Controller.InputTypes, EnemyInformation> Enemies => enemies;
 }
