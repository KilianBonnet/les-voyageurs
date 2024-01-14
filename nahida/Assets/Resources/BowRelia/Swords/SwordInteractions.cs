using UnityEngine;

public class SwordInteractions : MonoBehaviour
{

    [SerializeField] int attackDamage = 5;

    private void OnEnable()
    {
        TutorialEnemy.TutorialComplete += TutorialComplete;
    }

    private void TutorialComplete()
    {
        NetworkingInvoke.SendInvokeEvent(1);
        TutorialEnemy.TutorialComplete -= TutorialComplete;

    }

    public int GetAttackDamage()
    {
        return attackDamage;
    }
}

