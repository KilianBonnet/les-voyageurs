using UnityEngine;

public class VrMap : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.transform.CompareTag("Enemy")) {
            Destroy(other.gameObject);
            scoreManager.IncreaseScore(-200);
        }
    }
}
