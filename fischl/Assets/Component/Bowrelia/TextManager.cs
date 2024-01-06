using UnityEngine;

public class TextManager : MonoBehaviour
{

    [SerializeField] GameObject floatingText;

    private void OnEnable()
    {
        Slime.OnDeath += HandleDeath;
    }

    private void HandleDeath(Transform slimeTransform)
    {
        if (floatingText)
        {
            Instantiate(floatingText, slimeTransform.position, Quaternion.identity);
        }
    }
 
}
