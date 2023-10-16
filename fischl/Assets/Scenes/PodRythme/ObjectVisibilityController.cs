using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectVisibilityController : MonoBehaviour
{
    public BoxCollider2D nonPlayingZone;
    public float respawnTime = 3f;
    public Camera mainCamera;

    private void Start()
    {
    }

    private void RespawnObject()
    {
        Vector3 randomPosition = GetRandomPosition();
        if (!nonPlayingZone.bounds.Contains(randomPosition))
        {
            GameObject newObject = Instantiate(gameObject, randomPosition, Quaternion.identity);
            newObject.SetActive(true); // Assurez-vous que le nouvel objet est activé
        }
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
        // Désactive l'objet sur lequel l'utilisateur a cliqué.
        gameObject.SetActive(false);
        Invoke("RespawnObject", respawnTime);
    }
}
