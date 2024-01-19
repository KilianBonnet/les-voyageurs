using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        if (!source)
            source = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }


    public void ShowEndGame()
    {
        if (endGameUI)
            endGameUI.SetActive(true);
        source.clip = clip;
        source.Play();
        GameObject.Find("EnemySpawner").SetActive(false);
    }
}
