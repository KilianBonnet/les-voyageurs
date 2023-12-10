using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<Door> doors; //Modifier pour le script de la porte quand il sera fait
    [SerializeField] private List<Enemy> enemies;
    private int roomId;
    private bool hasPlayer;
    private bool isRoomClear;
    private int enemyCounter;

    private void Start() {
        roomId = int.Parse(name.Split(" ")[1]);
        enemyCounter = enemies.Count;

        EntryRoomTrigger.PlayerRoomEnterEvent += HandleEntryEvent;
        Enemy.EnemyKilledEvent += OnEnemyKilled;
    }


    void HandleEntryEvent(int roomId)
    {
        hasPlayer = this.roomId == roomId;

        if(!hasPlayer || isRoomClear)
            return;

        foreach (Enemy enemy in enemies) enemy.gameObject.SetActive(true);
        foreach (Door door in doors) door.CloseDoor();
    }

    private void OnEnemyKilled()
    {
        if(!hasPlayer)
            return;

        enemyCounter--;
        if(enemyCounter <= 0) OnRoomClear();
    }

    private void OnRoomClear() {
        foreach (Door door in doors) door.OpenDoor();
    }
}

