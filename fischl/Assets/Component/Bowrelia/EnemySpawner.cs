using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    private bool spawnAdditionalEnemies = false;


    public void StartGame()
    {
        InvokeRepeating("SpawnEnemy", 1f, 3f);
        Invoke("EnableAdditionalSpawn", 10f); // Enable additional spawns after 60 seconds
    }

    private void EnableAdditionalSpawn()
    {
        spawnAdditionalEnemies = true;
        Debug.Log("Stronger enemy are here");
    }

    private Vector2 GeneratePosition()
    {
        Vector2 position = new Vector2();
        position.y = Random.Range(-4f, 4f);
        if (position.y < 1) position.x = Random.Range(-8f, 8f);
        else position.x = Random.Range(0, 2) == 0 ? Random.Range(-8f, -4f) : Random.Range(8, 4);
        return position;
    }

    private void SpawnEnemy()
    {
        int randomIndex = spawnAdditionalEnemies ? Random.Range(0, enemyPrefabs.Count) : 0;
        GameObject enemy = Instantiate(enemyPrefabs[randomIndex]);
        enemy.transform.position = GeneratePosition();
        enemy.transform.Rotate(0, 0, Random.Range(0, 4) * 90);
    }
}
