using UnityEngine;

public class Door : MonoBehaviour
{
    public void OpenDoor()
    {
        gameObject.GetComponent<Animator>().SetBool("opening", true);
    }

    public void CloseDoor()
    {
        gameObject.GetComponent<Animator>().SetBool("opening", false);
    }

}
