using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class WebSocketClient : MonoBehaviour {
    public static WebSocketClient Instance;
    public static event Action<SocketMessage> OnSocketMessage;

    private WebSocket ws;
    [SerializeField] private string address = "ws://localhost:8080";
    [SerializeField] private string deviceType = "VR_Headset";

    [SerializeField] private int retryInterval = 1;
    private bool canRetry = true;

    private Queue<SocketMessage> messageQueue;
    private bool isReady;

    private void Start(){
        Instance = this;
        messageQueue = new Queue<SocketMessage>();
        ws = new WebSocket(address);
        ws.ConnectAsync();
        ws.OnMessage += OnMessage;
    }

    private void Update() {
        // Check if the connecting is alive
        if(!ws.IsAlive && canRetry) {
            isReady = false;
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
        SocketMessage message = JsonConvert.DeserializeObject<SocketMessage>(e.Data);

        if(isReady) {
            messageQueue.Enqueue(message);
            return;
        }
 
        if(message.op == SocketOP.HELLO_EVENT) {
            SendMessage(
                SocketOP.IDENTIFY_EVENT,
                new { device = deviceType }
            );
        }

        if(message.op == SocketOP.READY_EVENT) {
            Debug.Log("Client is ready!");
            isReady = true;
        }
    }

    public void SendMessage(int op, object data = null) {
        if(data != null) {
            var payload = new { 
                op,
                d = data
            };
            ws.Send(JsonConvert.SerializeObject(payload, Formatting.Indented));
        }
        else {
            var payload = new { op };
            ws.Send(JsonConvert.SerializeObject(payload, Formatting.Indented));
        }
            
    }
}