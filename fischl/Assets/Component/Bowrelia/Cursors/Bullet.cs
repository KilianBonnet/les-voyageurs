using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool isInit;
    private float speed;
    private Vector2 movement;
    private float timeAlive;

    public void Shoot(Vector2 firstCursorPosition, Vector2 secondCursorPosition) {
        Vector2 bulletPosition = transform.position;
        Vector2 bulletToFirstCursor = firstCursorPosition - bulletPosition;
        Vector2 bulletToSecondCursor = secondCursorPosition -  bulletPosition;

        Vector2 vectorSum = bulletToFirstCursor + bulletToSecondCursor;

        movement = vectorSum.normalized;
        speed = vectorSum.magnitude * .01f;
        isInit = true;
    }

    private void LateUpdate() {
        if(!isInit) return;
        timeAlive += Time.deltaTime;
        transform.Translate(movement * speed);
        
        if(timeAlive > 10)
            Destroy(gameObject);
    }
}
