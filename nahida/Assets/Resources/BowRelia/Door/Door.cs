using UnityEngine;

public class Door : MonoBehaviour
{
    public void OpenDoor()
    {
        gameObject.GetComponent<Animator>().SetBool("opening", true);
    }
}
