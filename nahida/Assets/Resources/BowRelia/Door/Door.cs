using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    private AudioSource source;

    private void Start() {
        animator = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
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

    public void OpenDoorSound()
    {
        if (source)   
            source.Play();   
    }
}
