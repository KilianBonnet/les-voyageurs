using UnityEngine;

public class WebTest : MonoBehaviour
{
    public void DestroySelf() {
        Destroy(gameObject);
    }

    public void OnBonus(BonusType bonusType) {
        Debug.Log("Receive bonus " + bonusType);
    }
}
