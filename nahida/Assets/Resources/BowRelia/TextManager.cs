using UnityEngine;

public class TextManager : MonoBehaviour
{

    [SerializeField] GameObject floatingText;

    private void OnEnable()
    {
        Enemy.OnDeathEvent += HandleDeath;
    }

    private void HandleDeath(Transform enemyTransform)
    {
        if (floatingText)
        {
            Instantiate(floatingText, enemyTransform);
        }
    }

}
