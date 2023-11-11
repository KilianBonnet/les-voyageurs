using UnityEngine;

enum RoomEvent {
    NONE,
    BOSS,
    SWORD,
    HEART,
    DOOR
}

public class Room : MonoBehaviour
{
    private GameObject vrPlayer;
    private ScoreManager scoreManager;

    [Space]
    [SerializeField] private GameObject eventIcon;
    [SerializeField] private RoomEvent eventType;

    [Space]
    [SerializeField] private Room topRoom;
    [SerializeField] private Room bottomRoom;
    [SerializeField] private Room leftRoom;
    [SerializeField] private Room rightRoom;

    [SerializeField] private bool canListen = false;

    private void Start() {
        scoreManager = GameObject.Find("Progress Bar").GetComponent<ScoreManager>();
        vrPlayer = GameObject.Find("VrPlayer");
    }

    public void SetRoomActive(GameObject vrPlayer) {
        Invoke("StartListening", .1f);
        this.vrPlayer = vrPlayer;
        vrPlayer.transform.position = transform.position;

        if(eventType == RoomEvent.NONE) {
            scoreManager.IncreaseScore(100);
            return;
        }

        if(eventType == RoomEvent.BOSS) scoreManager.IncreaseScore(500);
        
        eventIcon.SetActive(false);
    }

    public void StartListening() {
        canListen = true;
    }


    // Update is called once per frame
    void Update()
    {
        if(!canListen) return;

        if(Input.GetKeyDown(KeyCode.Z) && topRoom != null) {
            topRoom.SetRoomActive(vrPlayer);
            canListen = false;
        }

        if(Input.GetKeyDown(KeyCode.S) && bottomRoom != null) {
            bottomRoom.SetRoomActive(vrPlayer);
            canListen = false;
        }

        if(Input.GetKeyDown(KeyCode.Q) && leftRoom != null) {
            leftRoom.SetRoomActive(vrPlayer);
            canListen = false;
        }

        if(Input.GetKeyDown(KeyCode.D) && rightRoom != null) {
            rightRoom.SetRoomActive(vrPlayer);
            canListen = false;
        }
    }
}
