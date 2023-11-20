using UnityEngine;
using WebSocketSharp;

public class WebSocketClient : MonoBehaviour {
    [SerializeField] private string address;
    WebSocket ws;

    private void Start(){
        ws = new WebSocket(address);
        ws.Connect();

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message Received from "+((WebSocket)sender).Url+", Data : "+e.Data);
        };

    }

    private void Update() {
        if(ws == null) return;
        if(Input.GetKeyDown(KeyCode.P)) ws.Send("{\"input\": \"space\"}");
    }
}