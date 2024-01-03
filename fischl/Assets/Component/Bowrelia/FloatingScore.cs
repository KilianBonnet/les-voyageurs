using UnityEngine;

public class FloatingScore : MonoBehaviour
{
    public float DestroyTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
}
