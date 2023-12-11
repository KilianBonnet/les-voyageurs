using TMPro;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private ScoreManager scoreManager;

    public GameObject circlePrefab;
    public GameObject waitingCirclePrefab;
    public BowRenderer bowRenderer;
    public bool canShoot = true;

    [Space]
    [SerializeField] private TextMeshProUGUI scoreUi;
    private int score;
    
    private void Start() {
        scoreManager = GameObject.Find("Networking Score").GetComponent<ScoreManager>();
    }

    public void IncreaseScore(int value) {
        score += value;
        scoreUi.text = score.ToString();
        scoreManager.IncreaseScore(score);
    }
}
