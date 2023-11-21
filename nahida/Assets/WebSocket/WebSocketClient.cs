using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class WebSocketClient : MonoBehaviour {
    public static event Action<string> OnSocketMessage;

    private WebSocket ws;
    [SerializeField] private string address = "ws://localhost:8080";
    [SerializeField] private string deviceType = "VR_Headset";

    [SerializeField] private int retryInterval = 1;
    private bool canRetry = true;

    private Queue<string> messageQueue;
    private bool isReady;

    private void Start(){
        messageQueue = new Queue<string>();
        ws = new WebSocket(address);
        ws.ConnectAsync();
        ws.OnMessage += OnMessage;
    }

    private void Update() {
        // Check if the connecting is alive
        if(!ws.IsAlive && canRetry) {
            // Try to reconnect.
            Debug.LogWarning("Connection lost, retrying...");
            ws.ConnectAsync();
            canRetry = false;
            Invoke("EnableRetrying", retryInterval);
            return;
        }

        // Check if the client is identified by the server
        if(!isReady)
            return;

        for(int i = 0; i < messageQueue.Count; i++)
            OnSocketMessage.Invoke(messageQueue.Dequeue());
    }

    private void EnableRetrying() {
        canRetry = true;
    }

    private void OnMessage(object sender, MessageEventArgs e) {
        if(isReady) {
            messageQueue.Enqueue(e.Data);
            return;
        }
 
        SocketMessage message = JsonConvert.DeserializeObject<SocketMessage>(e.Data);

        if(message.op == 1) {
            var payload = new {
                op = 2,
                d = new {
                    device = deviceType
                }
            };
            ws.Send(JsonConvert.SerializeObject(payload, Formatting.Indented));
        }

        if(message.op == 3) {
            Debug.Log("Client is ready!");
            isReady = true;
        }
    }
}