using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectVisibilityController : MonoBehaviour
{
    public BoxCollider2D nonPlayingZone;
    public Camera mainCamera;

    private void RespawnObject()
    {
        Vector2 randomPosition = GetRandomPosition();
        while (nonPlayingZone.bounds.Contains(randomPosition)) { randomPosition = GetRandomPosition(); }

        gameObject.transform.position = randomPosition;
        //On active de nouveau l'objet après le temps d'apparition
        gameObject.SetActive(true);
        
    }

    private Vector2 GetRandomPosition()
    {
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        float x = Random.Range((-width+1) / 2, (width-1) / 2);
        float y = Random.Range((-height+1) / 2, (height-1) / 2);
        Vector2 randomPosition;

        do
        {
            x = Random.Range((-width + 1) / 2, (width - 1) / 2);
            y = Random.Range((-height + 1) / 2, (height - 1) / 2);
            randomPosition = new Vector2(x, y);
            
            Collider[] colliders = Physics.OverlapBox(randomPosition, new Vector2(1, 1), Quaternion.identity);

            // Vérifiez si un objet est détecté à la position aléatoire
            if (colliders.Length == 0 || !colliders[0].isTrigger)
            {
                break;
            }
        } while (true);
        return new Vector2(x, y);
    }
    private void OnMouseDown()
    {
        // Désactive l'objet sur lequel l'utilisateur a cliqué
        gameObject.SetActive(false);
        float respawnTime = Random.Range(2, 5);
        Invoke("RespawnObject", respawnTime);
    }
}
