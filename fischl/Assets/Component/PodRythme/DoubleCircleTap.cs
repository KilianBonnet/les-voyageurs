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

    public void OnMouseDown()
    {
        time = Time.time;
        Debug.Log(time);
        Debug.Log(otherTapCircle.time);
        if (otherTapCircle.time < 0)
            return;

        // Vérifie si les deux cercles ont été cliqués avec une différence de temps de 2 secondes
        if (Mathf.Abs(time - otherTapCircle.time) < clickDelay)
        {
            // Réinitialise les indicateurs
            gameObject.SetActive(false);
            otherTapCircle.gameObject.SetActive(false);

            // Déclenche le respawn des deux cercles après un délai de 5 secondes
            Invoke("RespawnObjects", 5f);
            Invoke("RespawnObjectsOther", 5f);
        }
        // Si perdu 
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