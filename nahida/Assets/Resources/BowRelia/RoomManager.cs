using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<Door> doors; //Modifier pour le script de la porte quand il sera fait
    [SerializeField] private List<EnemyScript> enemies;
    private int roomId;
    private bool isRoomClear;

    private void Start() {
        roomId = int.Parse(name.Split(" ")[1]);
        EntryRoomTrigger.PlayerRoomEnterEvent += HandleEntryEvent;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count == 0)
        {
            OpenExitDoor();
        }
        CheckDestroyedObjects();
    }

    void OpenExitDoor()
    {

    }

    void HandleEntryEvent(int roomId)
    {
        if(this.roomId != roomId)
            return;

        foreach (EnemyScript enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
        }
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

