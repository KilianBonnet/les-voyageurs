using System;
using UnityEngine;

public class NetworkingRoom : MonoBehaviour
{
    public static event Action<int> RoomChangeEvent;

    private void Start() {
        WebSocketClient.OnSocketMessage += OnSocketMessage;
    }

    private void OnSocketMessage(SocketMessage socketMessage) {
        if(socketMessage.op != SocketOP.ROOM_EVENT)
            return;

        RoomData d = socketMessage.d.ToObject<RoomData>();
        RoomChangeEvent.Invoke(d.room);
    }

    static public void SendRoomEvent(int room) {
        WebSocketClient.Instance.SendMessage(
            SocketOP.ROOM_EVENT, 
            new { room });
    }

}
