using UnityEngine;

public class Cursor
{
    public int fingerId;
    public readonly Zone originalZone;
    public readonly GameObject cursorObject;

    public Cursor(int fingerId, Zone originalZone, GameObject cursorObject) {
        this.fingerId = fingerId;
        this.originalZone = originalZone;
        this.cursorObject = cursorObject;
    }

    public Cursor(Zone originalZone, GameObject cursorObject) {
        fingerId = -1;
        this.originalZone = originalZone;
        this.cursorObject = cursorObject;
    }

}
