using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectVisibilityController : MonoBehaviour
{
    public BoxCollider2D nonPlayingZone;
    public float respawnTime = 3f;
    public Camera mainCamera;
    public GameObject tapCircles;

    private void Start()
    {
    }

    private void RespawnObject()
    {
        Vector3 randomPosition = GetRandomPosition();
        while (nonPlayingZone.bounds.Contains(randomPosition)) { randomPosition = GetRandomPosition(); }

        gameObject.transform.position = randomPosition;
       // GameObject newObject = Instantiate(gameObject, randomPosition, Quaternion.identity, tapCircles.transform);
        //On active le nouvel objet car celui que l'on a copié était desactivé
        gameObject.SetActive(true);
        
    }

    private Vector3 GetRandomPosition()
    {
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        float x = Random.Range(-width / 2, width / 2);
        float y = Random.Range(-height / 2, height / 2);

        return new Vector2(x, y);
    }
    private void OnMouseDown()
    {
        // Désactive l'objet sur lequel l'utilisateur a cliqué
        gameObject.SetActive(false);
        Invoke("RespawnObject", respawnTime);
    }
}
