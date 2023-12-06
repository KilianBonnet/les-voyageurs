using UnityEngine;

public class BonusEventHandler : MonoBehaviour
{
    [SerializeField] private ElementContainer bombContainer;
    private AudioSource bonusAudio;

    private void Start() {
        bonusAudio = GetComponent<AudioSource>();
    }

    public void OnBonus(BonusType bonus) {
        bonusAudio.Play();

        switch(bonus) {
            case BonusType.BOMB:
                bombContainer.IncreaseCount(1);
                break;
        }
    }
}
