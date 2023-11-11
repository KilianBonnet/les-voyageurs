using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int totalScore;
    private Slider slider;

    private void Start() {
        slider = GetComponent<Slider>();
    }

    public void IncreaseScore(int value) {
        totalScore += value;
        if(totalScore < 0) totalScore = 0;
        if(totalScore > slider.maxValue) totalScore = (int) slider.maxValue;

        slider.value = totalScore;

        if(totalScore >= slider.maxValue) {
            GameObject.Find("door_close").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("door_open").GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
