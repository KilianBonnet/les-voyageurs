using UnityEngine;
using System;

public class TutorialSlime : MonoBehaviour
{
    public static event Action<Cursor> OnTutorialDeath;
    [SerializeField] Zone _expectedZone;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Cursor cursor = other.gameObject.GetComponent<Cursor>();
        if (cursor == null || cursor.cursorType != CursorType.CURSOR) return;
        OnTutorialDeath.Invoke(cursor);
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
                if(cursor.originalZone == _expectedZone)
                {
                    OnTutorialDeath.Invoke(cursor);
                    Destroy(gameObject);
                }
            }
        }
    }
}
