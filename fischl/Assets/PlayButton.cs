using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour    
{
    public Animator animator;

    public void OnMouseDown()
    {
        animator.SetBool("start", true);
        // Trouver le GameManager dans la sc�ne et appeler sa m�thode LancerPartie
        EventManager gameManager = FindObjectOfType<EventManager>();
        if (gameManager != null)
        {
            gameManager.startGame();
        }
        else
        {
            Debug.LogError("GameManager non trouv� dans la sc�ne !");
        }
  
    }
}