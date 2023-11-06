using UnityEngine;

public class RotateTextAroundObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private Transform rotateAround;



    private void Update()
    {
        this.transform.RotateAround(rotateAround.position, Vector3.forward, rotationSpeed * Time.deltaTime);

    }
}
