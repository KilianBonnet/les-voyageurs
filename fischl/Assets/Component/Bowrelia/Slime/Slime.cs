using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private Transform objectiveAnchor;
    [SerializeField] private float speed = .5f;
    [SerializeField] private int score = 125;

    private void Start() {
        transform.Rotate(0, 0, Random.Range(0, 4) * 90);
        speed *= .01f;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(!other.CompareTag("Player"))
            return;

        Zone zone = other.gameObject.GetComponent<Zone>();
        zone.IncreaseScore(score);

        Destroy(other.gameObject);
        Destroy(gameObject);
    }

    private void FixedUpdate() {
        Vector2 direction = objectiveAnchor.position - transform.position;
        transform.Translate(direction.normalized * speed);
    }
}
