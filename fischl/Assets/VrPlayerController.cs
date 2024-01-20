using UnityEngine;

public class VrPlayerController : MonoBehaviour
{
    [SerializeField] Transform rotationArrow;

    public void OnRotationChange(Vector3 r)
    {
        r.z = -r.y;
        r.y = 0;
        rotationArrow.rotation = Quaternion.Euler(r);
    }
}
