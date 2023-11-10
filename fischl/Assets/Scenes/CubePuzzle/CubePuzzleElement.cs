using UnityEngine;
using System;

public class CubePuzzleElement : MonoBehaviour
{
    private GameObject draggedObject;
    private Vector3 initialPosition;

    float limiteXMin;
    float limiteYMin;
    float limiteXMax;
    float limiteYMax;

    public static event Action<(string, string)> OnMouseUpEvent; // Déclaration de l'événement

    void Start()
    {

        // Récupérer le composant Renderer de l'objet parent
        Renderer parentRenderer = transform.parent.parent.parent.GetComponent<Renderer>();

        if (parentRenderer != null)
        {
            // Coordonnées de la limite de l'objet parent
            limiteXMin = parentRenderer.bounds.min.x;
            limiteYMin = parentRenderer.bounds.min.y;
            limiteXMax = parentRenderer.bounds.max.x;
            limiteYMax = parentRenderer.bounds.max.y;
        }
        else
        {
            Debug.LogError("Le parent n'a pas de composant Renderer.");
        }
    }

    public void onTouchStart()
    {
        initialPosition = transform.position;
    }

    public void OnTouchEnd()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        Debug.Log(colliders.Length);
        gameObject.transform.position = initialPosition;
        foreach (Collider2D collider in colliders)
        {

            Debug.Log(collider);
            if (collider.gameObject != gameObject && collider.gameObject.transform.position.x < limiteXMax && collider.gameObject.transform.position.x > limiteXMin && collider.gameObject.transform.position.y < limiteYMax && collider.gameObject.transform.position.y > limiteYMin)
            {
                OnMouseUpEvent.Invoke((gameObject.name, collider.gameObject.name));
                return;
            }
        }
    }

    private Vector2 GetWorldPosition(Vector3 touchPosition)
    {
        return Camera.main.ScreenToWorldPoint(touchPosition);
    }
}