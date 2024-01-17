using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] Transform parent;

    private void Start()
    {
        if (!parent)
        {
            GameObject tmp = GameObject.Find("Puzzle");
            if (tmp)
                parent = tmp.transform;
        }
    }
    public void SwapElements(string[] elemNames)
    {
        GameObject[]  elems = SearchPuzzlePieces(elemNames[0], elemNames[1]);
        changeCoords(elems);
    }
    public void changeCoords(GameObject[] elems)
    {
        Vector2 vect1 = elems[0].transform.position;
        Vector2 vect2 = elems[1].transform.position;
        Vector2 tmp = vect2;
        elems[1].transform.position = vect1;
        elems[0].transform.position = tmp;
    }
    private GameObject[] SearchPuzzlePieces(string gameObjectName1, string gameObjectName2)
    {
        GameObject[] elems = new GameObject[2];
        int count = 0;

        foreach (Transform ligne in parent)
        {
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
}
