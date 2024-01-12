using UnityEngine;

public class DoorOpening : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Door door = animator.gameObject.GetComponentInParent<Door>();

        if (door)
        {
            door.OpenDoorSound();
        }
    }
}
