using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private List<Cursor> cursors;

    private void Update() {
        for (int i = 0; i <  Input.touchCount; i++) {
            Touch touch = Input.GetTouch(i);

            switch (touch.phase) {
                case TouchPhase.Began:
                    BeginTouch(touch);
                    break;

                case TouchPhase.Moved:
                    MoveCircle(touch);
                    break;
                
                case TouchPhase.Ended:
                    RemoveCircle(touch);
                    break;
            }
        }
    }

    private Zone GetZone(Vector2 worldPosition) {
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0);
        if(!hit.transform) return null;
        return hit.transform.GetComponent<Zone>();
    }

    private Vector2 GetWorldPosition(Vector3 touchPosition) {
        return Camera.main.ScreenToWorldPoint(touchPosition);
    }

    private void CreateCircle(Zone zone, Touch touch) {
        GameObject circle = Instantiate(zone.circlePrefab);
        circle.transform.position = GetWorldPosition(touch.position);
        circle.name = "Touch " + touch.fingerId;

        Cursor cursor = new Cursor(touch.fingerId, zone, circle);
        cursors.Add(cursor);
    }

    private void MoveCircle(Touch touch) {
        Cursor cursor = cursors.Find(cursor => cursor.fingerId == touch.fingerId);
        Zone zone = GetZone(GetWorldPosition(touch.position));
        GameObject cursorObject = cursor.cursorObject;

        // Check if the fingers is in the correct zone
        if(zone == null || zone != cursor.originalZone) {
            cursorObject.SetActive(false);
            return;
        }

        if(!cursorObject.activeSelf) cursorObject.SetActive(true);
        cursorObject.transform.position = GetWorldPosition(touch.position);
    }

    private void RemoveCircle(Touch touch) {
        Cursor cursor = cursors.Find(cursor => cursor.fingerId == touch.fingerId);
        Destroy(cursor.cursorObject);
        cursors.Remove(cursor);
    }

    private void BeginTouch(Touch touch) {
        Vector2 touchWorldPosition = GetWorldPosition(touch.position);
        Zone zone = GetZone(touchWorldPosition);

        // If the touch is not on a zone
        if(zone == null) return;

        CreateCircle(zone, touch);
    }
}
