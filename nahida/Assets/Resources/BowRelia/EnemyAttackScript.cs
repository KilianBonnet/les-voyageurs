using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{

    [SerializeField] Enemy thisEnemyDetails;

    public int GetThisEnemyDamages()
    {
        return thisEnemyDetails.GetDamage();
    } 
}
