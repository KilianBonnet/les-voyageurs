using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private Transform objectiveAnchor;
    [SerializeField] private float speed = .5f;

    private void Start() {
        speed *= .01f;
    }

    private void OnTriggerExit2D(Collider2D other) {
        Destroy(gameObject);
    }

    private void FixedUpdate() {
        Vector2 direction = objectiveAnchor.position - transform.position;
        transform.Translate(direction.normalized * speed);
    }
}
