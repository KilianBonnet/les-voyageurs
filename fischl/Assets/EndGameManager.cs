using UnityEngine;
using TMPro;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private GameObject endGameUI;

    [SerializeField] private TextMeshProUGUI greenScore;
    [SerializeField] private TextMeshProUGUI redScore;
    [SerializeField] private TextMeshProUGUI blueScore;

    [SerializeField] private GameObject greenTrophy;
    [SerializeField] private GameObject redTrophy;
    [SerializeField] private GameObject blueTrophy;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        if (!source)
            source = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        if(!greenScore)
            greenScore = GameObject.Find("Green Score").GetComponent<TextMeshProUGUI>();
        if(!redScore)
            redScore = GameObject.Find("Red Score").GetComponent<TextMeshProUGUI>();
        if(!blueScore)
            blueScore = GameObject.Find("Blue Score").GetComponent<TextMeshProUGUI>();
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
        else if (redValue >= greenValue && redValue >= blueValue)
            redTrophy.SetActive(true);
        else
            blueTrophy.SetActive(true);
    }
}
