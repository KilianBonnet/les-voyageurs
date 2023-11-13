using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private bool gameIsOver = false;
    public GameObject gameOverTexts;
    public GameObject tapCircles;

    public void startGame()
    {
        tapCircles.SetActive(true);
        // Appel de la méthode GameOver après une minute (60 secondes)
        Invoke("GameOver", 20f);
    }

    void GameOver()
    {
        if (!gameIsOver)
        {
            gameIsOver = true;
            Debug.Log("End Of the game!");
        }
        tapCircles.SetActive(false);
        gameOverTexts.SetActive(true);
    }
}
