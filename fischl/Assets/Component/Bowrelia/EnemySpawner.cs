using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    public void StartGame()
    {
        InvokeRepeating("SpawnEnemy", 1f, 3f);
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
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = GeneratePosition();
        enemy.transform.Rotate(0, 0, UnityEngine.Random.Range(0, 4) * 90);
    }
}
