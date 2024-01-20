using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BonusHandlerStruct {
    public string bonusName;
    public ElementContainer bonusContainer;
}

public class BonusEventHandler : MonoBehaviour
{
    [SerializeField] private List<BonusHandlerStruct> bonusHandlerList;
    [SerializeField] private Animator wheelAnimator;

    private Notification notification;
    private AudioSource bonusAudio;

    private void Start() {
        notification = FindObjectOfType<Notification>();
        NetworkingBonus.BonusEvent += OnBonusEvent;
        bonusAudio = GetComponent<AudioSource>();
    }

    public void OnBonusEvent(BonusType bonus) {
        bonusAudio.Play();
        if(bonus != BonusType.EMERALD) {
            wheelAnimator.SetTrigger("Notification");

            foreach (BonusHandlerStruct item in bonusHandlerList)
            {
                item.bonusContainer.IncreaseCount(1);
                notification.PlayNotification($"Obtained\n{item.bonusName}!", item.bonusName);
            }
        }
        else {
            notification.PlayNotification("Obtained\nemerald!", "emerald");
        } 

        
    }
}
