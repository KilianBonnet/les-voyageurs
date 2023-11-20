using System.Collections;
using TMPro;
using UnityEngine;

public class WebTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageUi;
    [SerializeField] private KeyCode eventKey = KeyCode.P;
    [SerializeField] WebSocketClient webSocketClient;

    private void Start() {
        messageUi.text = "Waiting for server ...";
        webSocketClient.OnSocketMessage += DisplayText;
    }

    private void DisplayText(string data) {
    }

    private void Update() {
        if(!Input.GetKeyDown(eventKey)) return;
        webSocketClient.SendEvent("test", "keyInput", "p");
    }

    private IEnumerator UpdateMessageUi(string text) {
        yield return null;
        messageUi.text = text;
    }
}
