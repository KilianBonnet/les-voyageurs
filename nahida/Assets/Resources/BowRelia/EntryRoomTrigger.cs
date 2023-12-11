using System;
using UnityEngine;

public class EntryRoomTrigger : MonoBehaviour
{
    public static event Action<int> PlayerRoomEnterEvent;
    private int roomId;

    private void Start() {
        roomId = int.Parse(name.Split(" ")[1]);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRoomEnterEvent.Invoke(roomId);
            NetworkingRoom.SendRoomEvent(roomId);
        }
    }
}
