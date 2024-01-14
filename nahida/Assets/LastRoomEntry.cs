using UnityEngine;

public class LastRoomEntry : MonoBehaviour
{
    private bool hasbeentriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !hasbeentriggered)
        {
            NetworkingInvoke.SendInvokeEvent(17);
            hasbeentriggered = true;
        }
    }
}
