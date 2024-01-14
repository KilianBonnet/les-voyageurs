using UnityEngine;

public class SwordInteractions : MonoBehaviour
{

    [SerializeField] int attackDamage = 5;

    public int GetAttackDamage()
    {
        return attackDamage;
    }
}

