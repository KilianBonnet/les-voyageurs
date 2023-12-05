using UnityEngine;

public class Room : MonoBehaviour
{
    private int roomId;
    private Transform vrPlayer;

    private void Start() {
        vrPlayer = GameObject.Find("VrPlayer").transform;
        roomId = int.Parse(name.Split(' ')[1]);
        NetworkingRoom.RoomChangeEvent += OnPlayerChangeRoom;
    }
    
    private void OnPlayerChangeRoom(int roomId) {
        if(this.roomId != roomId)
            return;
        vrPlayer.position = transform.position;
    }

}
