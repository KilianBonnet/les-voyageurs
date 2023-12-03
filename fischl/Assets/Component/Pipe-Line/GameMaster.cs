using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public CubePuzzleElement[] squares;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject playersIndications;
    [SerializeField] GameObject puzzle;



    void Start()
    {
        foreach(CubePuzzleElement cube in squares)
        {
            cube.SetCorrectPosition(cube.transform.position);

        }
        // Échange aléatoire des positions
        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(0, squares.Length);
            int j = randomIndex;
            while(j == randomIndex)
            {
                j = Random.Range(0, squares.Length);
            }
            squares[i].MixGame(squares[i].name, squares[j].name);
        }
    }


    void Update()
    {
        if (checkIsOver())
        {
            Debug.Log("Fin du jeu");
            gameOverText.SetActive(true);
            playersIndications.SetActive(false);
            puzzle.SetActive(false);
        }
    }

    //Tres moche mais fonctionnel
    bool checkIsOver()
    {
        foreach(CubePuzzleElement cube in squares)
        {
            if (!cube.IsWellPlaced())
            {
                return false;
            }
        }
        return true;
    }
}
