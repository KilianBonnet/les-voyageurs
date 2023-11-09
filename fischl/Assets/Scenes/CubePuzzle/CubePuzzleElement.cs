using UnityEngine;
using System;

public class CubePuzzleElement : MonoBehaviour
{

    private Vector3 offset;
    private bool isDragging = false;
    private GameObject draggedObject;
    private Vector3 initialPosition;
    public GameObject correspondingObject;

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
    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            draggedObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        draggedObject = gameObject;
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        initialPosition = gameObject.transform.position;
    }

    //TODO problème de correspondance quand on échange des cubes sur d'autres players
    //echanger aussi les cubes des autres joueurs ? Pas intuitif
    void OnMouseUp()
    {
        isDragging = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(draggedObject.transform.position, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != draggedObject && collider.gameObject.transform.position.x < limiteXMax && collider.gameObject.transform.position.x > limiteXMin && collider.gameObject.transform.position.y < limiteYMax && collider.gameObject.transform.position.y > limiteYMin )
            {

                OnMouseUpEvent.Invoke((gameObject.name, collider.gameObject.name));

                //Changer le coordonnees des elements du puzzle de la zone collective
                //Sur 4 matrices -> mm comportement donc recevoir un event OnChange() et on met le bloc qui a changé 
                //Donner le mm nom à tous les cubes du meme placement,
                //l'objet Squares contient le script qui va recevoir l'event
                // onChange va envoyer les noms des cubes qui s'echangent et chaque Squares va appliquer la modif

                /*GameObject otherCorrespondingObject = collider.gameObject.GetComponent<CubePuzzleElement>().correspondingObject;
                Vector3 vect1 = otherCorrespondingObject.transform.position;
                Vector3 vect2 = correspondingObject.transform.position;
                Vector3 tmp = vect2;
                correspondingObject.transform.position = vect1;
                otherCorrespondingObject.transform.position = tmp;*/
                break;
            }
            else
            {
                draggedObject.transform.position = initialPosition;
            }
        }
    }
}