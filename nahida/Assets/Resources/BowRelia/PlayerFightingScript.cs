using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFightingScript : MonoBehaviour
{
    [SerializeField] int hp = 10;

    private void Update()
    {
        if(hp <= 0)
        {
            //Player lose
        }
        else if (Input.GetMouseButtonDown(0)) // Input.GetButtonDown("BouttonAttaque")
        {
            AttackEnnemy();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            hp -= other.GetComponent<EnemyAttackScript>().GetThisEnemyDamages();
//            Debug.Log(hp);
        }
    }

    private void AttackEnnemy()
    {
        Debug.Log("Player try to attack");
        //If attack with a button then move sword
        //if the player move then nothing more to do
    }
}
