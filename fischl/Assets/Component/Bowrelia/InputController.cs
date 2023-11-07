using System;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private List<Cursor> cursors = new List<Cursor>();
    private List<WaitingCursor> waitingCursors = new List<WaitingCursor>();

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

    private void DeleteWaitingCursor(Zone zone) {
        WaitingCursor waitingCursor = waitingCursors.Find(cursor => cursor.originalZone == zone);
        waitingCursors.Remove(waitingCursor);
        Destroy(waitingCursor.gameObject);
    }

    /* TOOLS */
    private List<Cursor> GetZoneCursors(Zone zone) {
        return cursors.FindAll(cursor => cursor.originalZone == zone);
    }

    private Zone GetZone(Vector2 worldPosition) {
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0);
        if(hit.transform == null) return null;
        return hit.transform.GetComponent<Zone>();
    }

    private Vector2 GetWorldPosition(Vector3 touchPosition) {
        return Camera.main.ScreenToWorldPoint(touchPosition);
    }

    private Cursor CreateCircle(Zone zone, Touch touch) {
        GameObject circle = Instantiate(zone.circlePrefab);
        circle.transform.position = GetWorldPosition(touch.position);
        circle.name = "Touch " + touch.fingerId;

        Cursor cursor = new Cursor(touch.fingerId, zone, circle);
        cursors.Add(cursor);
        return cursor;
    }

    private void CreateBow(Zone zone, Cursor firstCursor, Touch newFinger) {
        Cursor secondCursor = CreateCircle(zone, newFinger);

        SetActivity(firstCursor, false);
        SetActivity(secondCursor, false);

        CreateWaitingCircle(zone, firstCursor, secondCursor);
    }

    private void CreateWaitingCircle(Zone zone, Cursor firstCursor, Cursor secondCursor) {
        GameObject circle = Instantiate(zone.waitingCirclePrefab);
        WaitingCursor waitingCursor = circle.GetComponent<WaitingCursor>();
        waitingCursor.Init(zone, firstCursor.cursorObject, secondCursor.cursorObject);
        waitingCursors.Add(waitingCursor);
    }

    private void DrawBow(Zone zone, Touch newFinger) {
        RaycastHit2D hit = Physics2D.Raycast(GetWorldPosition(newFinger.position), Vector2.zero, 0);
        if(hit.transform == null && hit.transform.GetComponent<WaitingCursor>()) return;
        CreateCircle(zone, newFinger);
        DeleteWaitingCursor(zone);

    }

    private void SetActivity(Cursor cursor, bool activity) {
        cursor.cursorObject.GetComponent<CircleCollider2D>().enabled = activity;
        cursor.cursorObject.GetComponent<CircleCollider2D>().enabled = activity;
    }


    /* TOUCH PHASES */
    private void BeginTouch(Touch touch) {
        Zone zone = GetZone(GetWorldPosition(touch.position));
        if(zone == null) return;

        List<Cursor> zoneCursors = GetZoneCursors(zone);
        switch(zoneCursors.Count) {
            case 0 :
                CreateCircle(zone, touch);
                break;
            case 1:
                CreateBow(zone, zoneCursors[0], touch);
                break;
            case 2:
                DrawBow(zone, touch);
                break;
        }
    }

    private void MoveCircle(Touch touch) {
        Cursor cursor = cursors.Find(cursor => cursor.fingerId == touch.fingerId);
        if(cursor == null) return;

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
        Cursor cursorToRemove = cursors.Find(cursor => cursor.fingerId == touch.fingerId);
        if(cursorToRemove == null) return;

        cursors.Remove(cursorToRemove);
        List<Cursor> zoneCursors = GetZoneCursors(cursorToRemove.originalZone);
        
        switch(zoneCursors.Count) {
            case 1:
                DeleteWaitingCursor(cursorToRemove.originalZone);
                SetActivity(GetZoneCursors(cursorToRemove.originalZone)[0], true);
                break;
            case 2:
                CreateWaitingCircle(cursorToRemove.originalZone, zoneCursors[0], zoneCursors[1]);
                break;
        }
        
        Destroy(cursorToRemove.cursorObject);
    }
}
