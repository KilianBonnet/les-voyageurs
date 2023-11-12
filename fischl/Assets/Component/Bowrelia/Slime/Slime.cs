using UnityEngine;

public class Slime : MonoBehaviour
{
    private Transform objectiveAnchor;
    private ScoreManager scoreManager;

    [SerializeField] private float speed = .5f;
    [SerializeField] private int score = 125;

    private void Start() {
        scoreManager = GameObject.Find("Progress Bar").GetComponent<ScoreManager>();
        objectiveAnchor = GameObject.Find("VRMap").transform;
        transform.Rotate(0, 0, Random.Range(0, 4) * 90);
        speed *= .01f;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(!other.CompareTag("Player"))
            return;

        Cursor cursor = other.gameObject.GetComponent<Cursor>();
        if(cursor == null || cursor.cursorType != CursorType.CURSOR) return;

        cursor.originalZone.IncreaseScore(score);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Player")) return;
            
        Cursor cursor = other.gameObject.GetComponent<Cursor>();
        if(cursor != null) {
            if(cursor.cursorType == CursorType.CURSOR) return;
            if(cursor.cursorType == CursorType.BULLET) {
                Destroy(other.gameObject);
                cursor.originalZone.IncreaseScore(score);
            }
        }
        else scoreManager.IncreaseScore(score);
        
        Destroy(gameObject);

    }

    private void FixedUpdate() {
        Vector2 direction = objectiveAnchor.position - transform.position;
        transform.Translate(direction.normalized * speed, Space.World);
    }
}
