using UnityEngine;
public class DoubleCircleTap : MonoBehaviour
{
    public BoxCollider2D nonPlayingZone;
    public Camera mainCamera;

    public DoubleCircleTap otherTapCircle;
    private float time = -1f;
    public float clickDelay = 2f;

    private void RespawnObjects()
    {
        Vector2 randomPosition = GetRandomPosition();
        while (nonPlayingZone.bounds.Contains(randomPosition)) { randomPosition = GetRandomPosition(); }

        gameObject.transform.position = randomPosition;
        gameObject.SetActive(true);

        time = -1f;
    }
    private Vector2 GetRandomPosition()
    {
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        float x = Random.Range((-width + 1) / 2, (width - 1) / 2);
        float y = Random.Range((-height + 1) / 2, (height - 1) / 2);
        Vector2 randomPosition;

        do
        {
            x = Random.Range((-width + 1) / 2, (width - 1) / 2);
            y = Random.Range((-height + 1) / 2, (height - 1) / 2);
            randomPosition = new Vector2(x, y);
            Collider[] colliders = Physics.OverlapBox(randomPosition, new Vector2(1, 1), Quaternion.identity);

            if (colliders.Length == 0)
            {
                break;
            }
        } while (true);
        return new Vector2(x, y);
    }

    public void OnTouchStart()
    {
        time = Time.time;
        if (otherTapCircle.time < 0)
            return;

        // Vérifie si les deux cercles ont été cliqués avec une différence de temps de clickDelay secondes
        if (Mathf.Abs(time - otherTapCircle.time) < clickDelay)
        {
            // Réinitialise les indicateurs
            gameObject.SetActive(false);
            otherTapCircle.gameObject.SetActive(false);

            float respawnTime = Random.Range(2, 5);
            Invoke("RespawnObjects", respawnTime);
            Invoke("RespawnObjectsOther", respawnTime);
        }
        // Si pas en mm temps
        else
        {
            time = -1;
            otherTapCircle.time = -1;
        }
    }

    private void RespawnObjectsOther()
    {
        Vector2 randomPosition = otherTapCircle.GetComponent<DoubleCircleTap>().GetRandomPosition();
        while (nonPlayingZone.bounds.Contains(randomPosition))
        {
           randomPosition = otherTapCircle.GetRandomPosition();
        }

        otherTapCircle.transform.position = randomPosition;
        otherTapCircle.gameObject.SetActive(true);
        otherTapCircle.GetComponent<DoubleCircleTap>().time = -1f;
    }
}