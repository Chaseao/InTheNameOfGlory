using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Room Deck", menuName = "Room Deck")]
public class RoomDeck : SerializedScriptableObject
{
    [SerializeField] List<Room> rooms;

    public List<Room> Rooms => rooms;
}
