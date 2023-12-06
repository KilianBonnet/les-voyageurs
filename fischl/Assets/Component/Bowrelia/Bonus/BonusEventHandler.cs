using UnityEngine;

public class BonusEventHandler : MonoBehaviour
{
    [SerializeField] private ElementContainer bombContainer;
    [SerializeField] private Animator wheelAnimator;

    private AudioSource bonusAudio;

    private void Start() {
        bonusAudio = GetComponent<AudioSource>();
    }

    public void OnBonus(BonusType bonus) {
        bonusAudio.Play();
        wheelAnimator.SetTrigger("Notification");

        switch(bonus) {
            case BonusType.BOMB:
                bombContainer.IncreaseCount(1);
                break;
        }
    }
}
