using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private bool gameIsOver = false;


    void Start()
    {
        // Appel de la m�thode GameOver apr�s une minute (60 secondes)
        Invoke("GameOver", 60f);
    }

    void GameOver()
    {
        if (!gameIsOver)
        {
            gameIsOver = true;
            Debug.Log("Game Over!");
        }
    }
}
