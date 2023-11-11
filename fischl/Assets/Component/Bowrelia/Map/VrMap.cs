using UnityEngine;

public class VrMap : MonoBehaviour
{
    ScoreManager scoreManager;

    private void Start() {
        scoreManager = GameObject.Find("Progress Bar").GetComponent<ScoreManager>();
    }


    private void OnTriggerEnter2D(Collider2D other) {

        if(other.transform.CompareTag("Enemy")) {
            scoreManager.IncreaseScore(-200);
            Destroy(other.gameObject);
        }
    }
}
