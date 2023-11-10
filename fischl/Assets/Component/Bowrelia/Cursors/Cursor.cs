using UnityEngine;

public enum CursorType {
    CURSOR,
    BULLET
}

public class Cursor : MonoBehaviour
{
    public int fingerId;
    public Zone originalZone;
    public CursorType cursorType = CursorType.CURSOR;

    public void InitCursor(int fingerId, Zone originalZone) {
        this.fingerId = fingerId;
        this.originalZone = originalZone;
    }

    public void InitCursor(Zone originalZone) {
        fingerId = -1;
        this.originalZone = originalZone;
    }

}
