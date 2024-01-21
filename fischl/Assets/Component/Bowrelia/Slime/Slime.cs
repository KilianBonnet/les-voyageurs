using System;
using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private Transform objectiveAnchor;
    private ScoreManager scoreManager;

    [SerializeField] private float speed = .5f;
    [SerializeField] private int score = 125;
    [SerializeField] private int health = 1;
    private bool isBlinking = false;

    public static event Action<Transform, Cursor, int> OnDeath;

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

        health -= 1;
        if (health > 0)
        {
            StartCoroutine(BlinkSlime());
        }
        else
        {
            OnDeath?.Invoke(transform, cursor, score);
            if (score > 0) cursor.originalZone.IncreaseScore(score);
            Destroy(gameObject);
        }
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
                health -= 1;
                if (health <= 0)
                {
                    Destroy(other.gameObject);
                    if (score > 0) cursor.originalZone.IncreaseScore(score);
                }
                else
                {
                    StartCoroutine(BlinkSlime());
                }
            }
        }
        else scoreManager.IncreaseScore(score);
        OnDeath?.Invoke(transform, cursor, score);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Vector2 direction = objectiveAnchor.position - transform.position;
        transform.Translate(direction.normalized * speed, Space.World);
    }

    private IEnumerator BlinkSlime()
    {
        if (isBlinking) yield break; // Already blinking, exit coroutine

        isBlinking = true;

        SpriteRenderer slimeRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = slimeRenderer.color;

        for (int i = 0; i < 4; i++)
        {
            slimeRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            yield return new WaitForSeconds(0.125f);
            slimeRenderer.color = originalColor;
            yield return new WaitForSeconds(0.125f);
        }

        isBlinking = false;
    }
}
