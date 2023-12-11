using UnityEngine;

public class Room : MonoBehaviour
{
    private int roomId;
    private Transform vrPlayer;
    private bool hasPlayer;

    private void Start() {
        vrPlayer = GameObject.Find("VrPlayer").transform;
        roomId = int.Parse(name.Split(' ')[1]);
        hasPlayer = roomId == 0;

        NetworkingRoom.RoomChangeEvent += OnPlayerChangeRoom;
        NetworkingBonus.BonusEvent += OnBonusEvent;
    }
    
    private void OnPlayerChangeRoom(int roomId) {
        hasPlayer = this.roomId == roomId;

        if(!hasPlayer)
            return;
            
        vrPlayer.position = transform.position;
    }

    public void OnBonusEvent(BonusType bonusType) {
        if(!hasPlayer) return;

        foreach(Transform child in transform)
            Destroy(child.gameObject);
    }
}
