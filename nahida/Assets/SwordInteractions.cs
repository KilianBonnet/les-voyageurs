using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInteractions : MonoBehaviour
{
    [SerializeField] int attackDamage = 5;

    public int GetAttackDamage()
    {
        return attackDamage;
    }
}

