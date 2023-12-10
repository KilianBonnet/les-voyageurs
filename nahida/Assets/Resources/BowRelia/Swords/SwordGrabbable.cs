using UnityEngine;

public class SwordGrabbable : MonoBehaviour
{
    private void Start() {
        if(transform.GetComponent<OVRGrabbable>().isGrabbed) {

        }
    }
}
