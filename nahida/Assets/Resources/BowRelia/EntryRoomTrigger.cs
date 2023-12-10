using System;
using UnityEngine;

public class EntryRoomTrigger : MonoBehaviour
{
    public static event Action PlayerEnter;

    private void Start() {
        PlayerEnter += ok;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerEnter.Invoke();

            int roomId = int.Parse(name.Split(" ")[1]);
            NetworkingRoom.SendRoomEvent(roomId);
        }
    }

    public void ok(){

    }
}
