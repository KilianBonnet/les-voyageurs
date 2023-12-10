using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI scoreUi;

    public void IncreaseScore(int value) {
        NetworkingScore.SendScoreEvent(value);
    }

    public void UpdateScore(int score) {
        scoreUi.text = $"Score: {score}";
        slider.value = score;
        
        if(score >= slider.maxValue) {
            GameObject.Find("door_close").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("door_open").GetComponent<SpriteRenderer>().enabled = true;
            NetworkingInvoke.SendInvokeEvent(2);
        }
    }
}
