using UnityEngine;
using System;

public class CubePuzzleElement : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 correctPosition;

    private float limiteXMin;
    private float limiteYMin;
    private float limiteXMax;
    private float limiteYMax;

    private bool currentlyTouch = false;

    // Evenement de fin de drag donc changement de position
    public static event Action<(string, string)> OnMouseUpEvent;
    //Indicateur de couleur du joueur qui bouge un objet -> methode radar
    public static event Action<(string, string)> OnMouseDownEvent;

    public static event Action<string> OnMouseDownEventNoPositionChange;

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

    public void OnTouchStart()
    {
        initialPosition = transform.position;
        //On cherche a savoir dans quelle zone de joueur nous sommes
        OnMouseDownEvent.Invoke((gameObject.name, transform.parent.parent.parent.name));

    }

    public void OnTouchEnd()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        gameObject.transform.position = initialPosition;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.gameObject.transform.position.x < limiteXMax && collider.gameObject.transform.position.x > limiteXMin && collider.gameObject.transform.position.y < limiteYMax && collider.gameObject.transform.position.y > limiteYMin)
            {
                if (!collider.gameObject.GetComponent<CubePuzzleElement>().GetCurrentlyTouch())
                {
                    OnMouseUpEvent.Invoke((gameObject.name, collider.gameObject.name));
                }
            }
        }
        OnMouseDownEventNoPositionChange.Invoke(gameObject.name);
    }

    public void SetCurrentlyTouch(bool isTouched)
    {
        currentlyTouch = isTouched;
    }

    public bool GetCurrentlyTouch()
    {
        return currentlyTouch;
    }

    public void MixGame(string square1Name, string square2Name) {
        OnMouseUpEvent.Invoke((square1Name, square2Name));
    }

    public bool IsWellPlaced()
    {
        return (transform.position.x == correctPosition.x) && (transform.position.y == correctPosition.y);
    }
    
    public void SetCorrectPosition(Vector3 position)
    {
        correctPosition = position;
    }
}