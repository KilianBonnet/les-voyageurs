using UnityEngine;

public class MoveItemToPlayer : MonoBehaviour
{
    public Transform player;
    public float distanceMax = 4f;
    public float timeBeforeMoving = 5f;

    private float time = 0f;
    private bool firstOccurence = true;
    public Transform swordContainer;

    void Update()
    {
        Debug.Log(time);
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > distanceMax)
        {
            if (firstOccurence)
            {
                timeBeforeMoving = time + 5f;
                firstOccurence = false;
            }
            time += Time.deltaTime;

            if (time >= timeBeforeMoving)
            {
                transform.position = swordContainer.transform.position;
                transform.rotation = swordContainer.rotation;
                firstOccurence = true;
            }
        }
        else
        {
            firstOccurence = true;
        }
    }
}
