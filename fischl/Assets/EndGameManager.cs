using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private GameObject endGameUI;

    public void ShowEndGame()
    {
        if (endGameUI)
            endGameUI.SetActive(true);
        GameObject.Find("EnemySpawner").SetActive(false);
    }
}
