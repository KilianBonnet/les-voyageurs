using UnityEngine;

public class WaitingCursor : MonoBehaviour
{
    private bool isInit;

    [HideInInspector] public Zone originalZone;
    private GameObject firstCursor;
    private GameObject secondCursor;

    public void Init(Zone originalZone, GameObject firstCursor, GameObject secondCursor) {
        isInit = true;
        this.originalZone = originalZone;
        this.firstCursor = firstCursor;
        this.secondCursor = secondCursor;
    }

    private void LateUpdate() {
        if(isInit) transform.position = (firstCursor.transform.position + secondCursor.transform.position) / 2f;
    }
}
