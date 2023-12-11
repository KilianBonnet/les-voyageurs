using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInteractions : MonoBehaviour
{
    private bool hasBeenGrabbedOnce;
    private Vector3 initialPosition;

    [SerializeField] Door firstDoor;
    [SerializeField] int attackDamage = 5;

    private FixedJoint joint;
    private bool isAttached = false;


    private void Start() {
        initialPosition = transform.position;
    }

    private void Update() {
        if(!hasBeenGrabbedOnce && Vector3.Distance(initialPosition, transform.position) > .1) {
            hasBeenGrabbedOnce = true;
            firstDoor.OpenDoor();
            NetworkingInvoke.SendInvokeEvent(1);
        }

        if(!(Vector3.Distance(initialPosition, transform.position) > .1) && hasBeenGrabbedOnce)
        {

        }
  
    }

    public int GetAttackDamage()
    {
        return attackDamage;
    }
    
    
}

