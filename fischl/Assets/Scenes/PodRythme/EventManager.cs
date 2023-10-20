using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private bool gameIsOver = false;
    public GameObject gameOverTexts;
    public GameObject tapCircles;



    void Start()
    {
        // Appel de la méthode GameOver après une minute (60 secondes)
        Invoke("GameOver", 60f);
    }

    void GameOver()
    {
        if (!gameIsOver)
        {
            gameIsOver = true;
            Debug.Log("Game Over!");
        }
        tapCircles.SetActive(false);
        gameOverTexts.SetActive(true);
    }
}
