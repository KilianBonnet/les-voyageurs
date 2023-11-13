using UnityEngine;

public class MatrixCoordinator : MonoBehaviour
{
    private void OnEnable()
    {
        CubePuzzleElement.OnMouseUpEvent += HandleMouseUpEvent; // Abonnement à l'événement
        CubePuzzleElement.OnMouseDownEvent += HandleOnSelectElementColorChange;
        CubePuzzleElement.OnMouseDownEventNoPositionChange += HandleNoPositionChanged;
    }

    private void OnDisable()
    {
        CubePuzzleElement.OnMouseUpEvent -= HandleMouseUpEvent; // Désabonnement de l'événement
        CubePuzzleElement.OnMouseDownEvent -= HandleOnSelectElementColorChange;
        CubePuzzleElement.OnMouseDownEventNoPositionChange -= HandleNoPositionChanged;
    }


    private void HandleMouseUpEvent((string, string) objectNames)
    {
        string gameObject1 = objectNames.Item1;
        string gameObject2 = objectNames.Item2;

        GameObject[] elems = RechercherElements(gameObject1, gameObject2);

        changeCoords(elems);
        if(transform.parent.name != "PuzzlePieces")
        {
            changeIsTouchOfSquare(false, elems[0]);
            changeIsTouchOfSquare(false, elems[1]);
        }
    }

    private void changeCoords(GameObject[] elems)
    {
        Vector2 vect1 = elems[0].transform.position;
        Vector2 vect2 = elems[1].transform.position;
        Vector2 tmp = vect2;
        elems[1].transform.position = vect1;
        elems[0].transform.position = tmp;

        changeColor(elems[0].GetComponent<Renderer>(), "initial");
        changeColor(elems[1].GetComponent<Renderer>(), "initial");
    }

    //Change bool IsTouch of the square to make them able to be moved again or not
    private void changeIsTouchOfSquare(bool value, GameObject square)
    {
        square.GetComponent<CubePuzzleElement>().setCurrentlyTouch(value);
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
                    elems[count] = elem.gameObject;
                    count++;
                    if (count == 1 && (gameObjectName1 == null || gameObjectName2 == null))
                        return elems;
                    if (count == 2) return elems;
                }
            }
        }
        return null;
    }

    private void HandleOnSelectElementColorChange((string,string) elemToChangeColor)
    {
        string square = elemToChangeColor.Item1;
        string playerArea = elemToChangeColor.Item2;
        GameObject squareInThisArea = RechercherElements(square, null)[0];
        Renderer squareRendererToChange = squareInThisArea.GetComponent<Renderer>();

        changeColor(squareRendererToChange, playerArea);
        if (transform.parent.name != "PuzzlePieces")
        {
            changeIsTouchOfSquare(true, squareInThisArea);
        }
    }

    private void changeColor(Renderer squareRendererToChange, string playerArea)
    {
        if (transform.parent.name.Equals("PuzzlePieces"))
        {
            return;
        }
        else if (playerArea.Equals("Area1"))
        {
            squareRendererToChange.material.color = Color.red;
        }
        else if (playerArea.Equals("Area2"))
        {
            squareRendererToChange.material.color = Color.green;
        }
        else if (playerArea.Equals("Area3"))
        {
            squareRendererToChange.material.color = Color.blue;
        }
        else
        {
            squareRendererToChange.material.color = new Color(1,1,1);
        }
    }

    private void HandleNoPositionChanged(string square)
    {
        GameObject squareInThisArea = RechercherElements(square, null)[0];
        Renderer squareRendererToChange = squareInThisArea.GetComponent<Renderer>();

        changeColor(squareRendererToChange, "initial");

        if (transform.parent.name != "PuzzlePieces")
        {
            changeIsTouchOfSquare(false, squareInThisArea);
        }
    }
}
