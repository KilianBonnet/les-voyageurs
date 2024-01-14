using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyBody : MonoBehaviour
{
    [SerializeField] TutorialEnemy parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerSword"))
        {
            parent.TakeDamage(other.GetComponent<SwordInteractions>().GetAttackDamage());
        }
    }
}
