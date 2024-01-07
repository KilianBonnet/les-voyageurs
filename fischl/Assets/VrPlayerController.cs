using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrPlayerController : MonoBehaviour
{
    public void OnRotationChange(Vector3 r)
    {
        r.z = -r.y;
        r.y = 0;
        transform.rotation = Quaternion.Euler(r);
    }
}
