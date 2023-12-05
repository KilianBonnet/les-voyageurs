using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{

    [SerializeField] EnemyScript thisEnemyDetails;

    public int GetThisEnemyDamages()
    {
        return thisEnemyDetails.GetDamage();
    } 
}
