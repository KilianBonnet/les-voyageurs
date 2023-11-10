using UnityEngine;
using System;

public class MatrixCoordinator : MonoBehaviour
{
    private void OnEnable()
    {
        CubePuzzleElement.OnMouseUpEvent += HandleMouseUpEvent; // Abonnement à l'événement
    }

    private void OnDisable()
    {
        CubePuzzleElement.OnMouseUpEvent -= HandleMouseUpEvent; // Désabonnement de l'événement
    }


    private void HandleMouseUpEvent((string, string) objectNames)
    {
        string gameObject1 = objectNames.Item1;
        string gameObject2 = objectNames.Item2;

        GameObject[] elems = RechercherElements(gameObject1, gameObject2);

        Vector2 vect1 = elems[0].transform.position;
        Vector2 vect2 = elems[1].transform.position;
        Vector2 tmp = vect2;
        elems[1].transform.position = vect1;
        elems[0].transform.position = tmp;
    }


    private GameObject[] RechercherElements(string gameObjectName1, string gameObjectName2)
    {
        Transform parent = transform; // Récupérer le transform de l'objet parent de la matrice
        GameObject[] elems = new GameObject[2];
        int count = 0;

        // Parcourir toutes les lignes de l'objet parent
        foreach (Transform ligne in parent)
        {
            // Parcourir tous les objets enfants de la ligne
            foreach (Transform elem in ligne)
            {
                if (elem.name == gameObjectName1 || elem.name == gameObjectName2)
                {
                    elems[count] = elem.gameObject; // Renvoyer l'objet s'il correspond
                    count++;
                    if (count == 2) return elems;
                }
            }
        }
        return null; // Renvoyer null si aucun élément correspondant n'est trouvé
    }
}
