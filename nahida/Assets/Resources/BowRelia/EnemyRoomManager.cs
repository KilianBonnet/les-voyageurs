using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomManager : RoomManager
{
    [SerializeField] private List<Enemy> enemies;
    private int enemyCounter;
    

    private new void Start() {
        base.Start();
        Enemy.EnemyKilledEvent += OnEnemyKilled;
        enemyCounter = enemies.Count;
        foreach (Enemy enemy in enemies) enemy.gameObject.SetActive(false);
    }

    protected override void OnPlayerEnterRoom() {
        if(isRoomClear)
            return;
        foreach (Enemy enemy in enemies) enemy.gameObject.SetActive(true);
    }

    private void OnEnemyKilled() {
        enemyCounter--;
        if(enemyCounter <= 0) OpenDoors();
    }
}
