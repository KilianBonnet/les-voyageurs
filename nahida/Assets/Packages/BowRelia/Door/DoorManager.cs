using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public void OpenDoor()
    {
        gameObject.GetComponent<Animator>().SetBool("opening", true);
    }
}
