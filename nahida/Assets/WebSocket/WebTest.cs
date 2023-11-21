using TMPro;
using UnityEngine;

public class WebTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageUi;
    [SerializeField] private KeyCode eventKey = KeyCode.P;

    private void Start() {
        messageUi.text = "Waiting for server ...";
        WebSocketClient.OnSocketMessage += DisplayText;
    }

    private void DisplayText(string text) {
        messageUi.text = text;
    }

    private void Update() {
        if(!Input.GetKeyDown(eventKey)) return;
    }
}
