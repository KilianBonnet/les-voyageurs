using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<Door> doors;
    [HideInInspector] public bool hasPlayer {get; private set; }
    [HideInInspector] public bool isRoomClear {get; protected set; }
    [HideInInspector] public int roomId {get; private set; }
    
    protected void Start() {
        roomId = int.Parse(name.Split(" ")[1]);
        EntryRoomTrigger.PlayerRoomEnterEvent += HandleEntryEvent;
    }

    private void HandleEntryEvent(int roomId)
    {
        hasPlayer = this.roomId == roomId;
        if(!hasPlayer) return;
        
        NetworkingRoom.SendRoomEvent(roomId);
        if(!isRoomClear) CloseDoors();
        OnPlayerEnterRoom();
    }

    protected virtual void OnPlayerEnterRoom() { }

    public void CloseDoors() {
        foreach (Door door in doors) door.CloseDoor();
    }

    public void OpenDoors() {
        isRoomClear = true;
        foreach (Door door in doors) door.OpenDoor();
    }
}

