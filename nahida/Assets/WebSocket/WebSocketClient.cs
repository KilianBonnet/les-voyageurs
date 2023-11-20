using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using System;

public class WebSocketClient : MonoBehaviour {
    [SerializeField] private string address;
    public event Action<string> OnSocketMessage;

    WebSocket ws;

    private void Start(){
        ws = new WebSocket(address);
        ws.Connect();

        ws.OnMessage += (sender, e) =>
        {
            OnSocketMessage.Invoke(e.Data);
        };
    }

    public void SendEvent(string sceneName, string eventName, object eventData) {
        var payload = new
        {
            scene = sceneName,
            eventName,
            data = eventData
        };

        try {
            ws.Send(JsonConvert.SerializeObject(payload, Formatting.Indented));
        }
        catch (Exception e){
            Debug.LogError(e.Message);
        }
    }

    public void SendToMainThread() {
        
    }
}