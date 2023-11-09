using TMPro;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public GameObject circlePrefab;
    public GameObject waitingCirclePrefab;
    public BowRenderer bowRenderer;
    public bool canShoot = true;

    [Space]
    [SerializeField] private TextMeshProUGUI scoreUi;
    private int score;

    public void IncreaseScore(int value) {
        score += value;
        scoreUi.text = score.ToString();
    }
}
