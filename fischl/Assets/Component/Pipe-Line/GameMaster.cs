using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public CubePuzzleElement[] squares;
    void Start()
    {
        // Échange aléatoire des positions
        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(0, squares.Length);
            int j = randomIndex;
            while(j == randomIndex)
            {
                j = Random.Range(0, squares.Length);
            }
            squares[i].mixGame(squares[i].name, squares[j].name);
        }
    }
}
