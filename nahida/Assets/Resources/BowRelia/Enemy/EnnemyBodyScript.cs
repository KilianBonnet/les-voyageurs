using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyBodyScript : MonoBehaviour
{
    [SerializeField] Enemy parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerSword"))
        {
            parent.TakeDamage(other.GetComponent<SwordInteractions>().GetAttackDamage());
        }
    }
}
