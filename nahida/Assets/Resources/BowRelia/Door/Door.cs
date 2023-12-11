using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    private void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    public void OpenDoor()
    {
        animator.SetBool("opening", true);
    }

    public void CloseDoor()
    {
        Debug.Log("Closing " + name);
        animator.SetBool("opening", false);
    }

}
