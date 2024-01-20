using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    private Animator animator;
    [SerializeField] TextMeshProUGUI messageUi;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void PlayNotification(string message, string childName) {
        messageUi.text = message;
        animator.SetTrigger("showNotification");

        foreach (Transform child in transform) 
            child.gameObject.SetActive(child.name == "Canvas" || child.name == childName);
    }

    public void ResetNotification() {
        messageUi.text = "";
        foreach (Transform child in transform) 
            child.gameObject.SetActive(false);
    }
}
