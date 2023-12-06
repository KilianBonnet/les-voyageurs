using System;
using UnityEngine;

public class EntryRoomTrigger : MonoBehaviour
{
    public static event Action PlayerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerEnter.Invoke();
        }
    }
}
