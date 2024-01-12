using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Door door = animator.gameObject.GetComponentInParent<Door>();

        if (door)
        {
            Debug.Log("Coucou");
            door.OpenDoorSound();
        }
    }
}
