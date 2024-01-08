using System;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private Transform objectiveAnchor;
    private ScoreManager scoreManager;

    [SerializeField] private float speed = .5f;
    [SerializeField] private int score = 125;
    public static event Action<Transform, Cursor> OnDeath;

    private void Start()
    {
        scoreManager = GameObject.Find("Networking Score").GetComponent<ScoreManager>();
        objectiveAnchor = GameObject.Find("VRMap").transform;
        speed *= .01f;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Cursor cursor = other.gameObject.GetComponent<Cursor>();
        if (cursor == null || cursor.cursorType != CursorType.CURSOR) return;
        OnDeath.Invoke(transform, cursor);
        cursor.originalZone.IncreaseScore(score);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Cursor cursor = other.gameObject.GetComponent<Cursor>();
        if (cursor != null)
        {
            if (cursor.cursorType == CursorType.CURSOR) return;
            if (cursor.cursorType == CursorType.BULLET)
            {
                Destroy(other.gameObject);
                if (score > 0) cursor.originalZone.IncreaseScore(score);
            }
        }
        else scoreManager.IncreaseScore(score);
        OnDeath.Invoke(transform, cursor);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Vector2 direction = objectiveAnchor.position - transform.position;
        transform.Translate(direction.normalized * speed, Space.World);
    }
}
