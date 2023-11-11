using UnityEngine;

enum RoomEvent {
    NONE,
    BOSS,
    SWORD,
    HEART
}

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject vrPlayer;

    [Space]
    [SerializeField] private GameObject eventIcon;
    [SerializeField] private RoomEvent eventType;

    [Space]
    [SerializeField] private Room topRoom;
    [SerializeField] private Room bottomRoom;
    [SerializeField] private Room leftRoom;
    [SerializeField] private Room rightRoom;

    public void SetRoomActive(GameObject vrPlayer) {
        this.vrPlayer = vrPlayer;
        vrPlayer.transform.position = transform.position;

        if(eventType == RoomEvent.NONE) 
            return;
        
        eventIcon.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(vrPlayer == null) return;

        if(Input.GetKeyDown(KeyCode.Z) && topRoom != null) {
            topRoom.SetRoomActive(vrPlayer);
            vrPlayer = null;
        }

        if(Input.GetKeyDown(KeyCode.S) && bottomRoom != null) {
            bottomRoom.SetRoomActive(vrPlayer);
            vrPlayer = null;
        }

        if(Input.GetKeyDown(KeyCode.Q) && leftRoom != null) {
            leftRoom.SetRoomActive(vrPlayer);
            vrPlayer = null;
        }

        if(Input.GetKeyDown(KeyCode.D) && rightRoom != null) {
            rightRoom.SetRoomActive(vrPlayer);
            vrPlayer = null;
        }
    }
}
