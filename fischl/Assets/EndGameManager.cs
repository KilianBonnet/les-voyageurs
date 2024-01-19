using UnityEngine;
using TMPro;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private GameObject endGameUI;

    private TextMeshPro greenScore;
    private TextMeshPro redScore;
    private TextMeshPro blueScore;

    [SerializeField] private GameObject greenTrophy;
    [SerializeField] private GameObject redTrophy;
    [SerializeField] private GameObject blueTrophy;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        if (!source)
            source = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        greenScore = GameObject.Find("Green Score").GetComponent<TextMeshPro>();
        redScore = GameObject.Find("Red Score").GetComponent<TextMeshPro>();
        blueScore = GameObject.Find("Blue Score").GetComponent<TextMeshPro>();
    }


    public void ShowEndGame()
    {
        if (endGameUI)
            endGameUI.SetActive(true);
        RewardBestPlayer();
        source.clip = clip;
        source.Play();
        GameObject.Find("EnemySpawner").SetActive(false);
    }

    private void RewardBestPlayer()
    {
        int greenValue = int.Parse(greenScore.text);
        int redValue = int.Parse(redScore.text);
        int blueValue = int.Parse(blueScore.text);
        if (greenValue >= redValue && greenValue >= blueValue)
            greenTrophy.SetActive(true);
        if (redValue >= greenValue && redValue >= blueValue)
            redTrophy.SetActive(true);
        if (blueValue >= greenValue && blueValue >= redValue)
            blueTrophy.SetActive(true);
    }
}
