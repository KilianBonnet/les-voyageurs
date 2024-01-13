using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInteractions : MonoBehaviour
{
    private bool hasBeenGrabbedOnce;
    private Vector3 initialPosition;

    [SerializeField] int attackDamage = 5;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (!hasBeenGrabbedOnce && Vector3.Distance(initialPosition, transform.position) > .1)
        {
            hasBeenGrabbedOnce = true;
            NetworkingInvoke.SendInvokeEvent(1);
        }
    }

    public int GetAttackDamage()
    {
        return attackDamage;
    }
}

