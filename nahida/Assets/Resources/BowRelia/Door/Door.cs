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
        animator.SetBool("opening", false);
    }

}
