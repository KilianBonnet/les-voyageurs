using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour    
{
    public Animator animator;

    public void OnMouseDown()
    {
        animator.SetBool("start", true);
        // Trouver le GameManager dans la scène et appeler sa méthode LancerPartie
        EventManager gameManager = FindObjectOfType<EventManager>();
        if (gameManager != null)
        {
            gameManager.startGame();
        }
        else
        {
            Debug.LogError("GameManager non trouvé dans la scène !");
        }
  
    }
}