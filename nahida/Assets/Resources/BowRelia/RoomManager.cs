using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] EntryRoomTrigger entryRoomDetector;
    [SerializeField] DoorManager exitdoor; //Modifier pour le script de la porte quand il sera fait
    [SerializeField] List<EnemyScript> enemies;

    private void OnEnable()
    {
        EntryRoomTrigger.PlayerEnter += HandleEntryEvent;
    }

    // Update is called once per frame
    void Update()
    {
        //Si tous les ennemies ont été vaincus alors ouverture de la sortie
        if (enemies.Count == 0)
        {
            OpenExitDoor();
        }
        CheckDestroyedObjects();
    }

    void OpenExitDoor()
    {
        if (exitdoor != null)
        {
            exitdoor.OpenDoor();
        }
    }

    void HandleEntryEvent()
    {
        //Si le joueur entre dans la pièce, alors spawn des ennemies
        Debug.Log("Player has entered the room");
        foreach (EnemyScript enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
        }

        EntryRoomTrigger.PlayerEnter -= HandleEntryEvent;

    }

    private void CheckDestroyedObjects()
    {
        int enemiesSize = enemies.Count;
        int i = 0;
        while(i < enemiesSize)
        {
            EnemyScript enemy = enemies[i];

            if (enemy == null)
            {
                enemies.RemoveAt(i);
            }
            i++;
        }
    }
}

