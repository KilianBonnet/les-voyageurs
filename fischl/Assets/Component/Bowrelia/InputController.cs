using System.Collections;
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


    /* TOOLS */
    private List<Cursor> GetZoneCursors(Zone zone) {
        return cursors.FindAll(cursor => cursor.originalZone == zone);
    }

    private Zone GetZone(Vector2 worldPosition) {
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0);
        if(hit.transform == null) return null;

        Zone zone = hit.transform.GetComponent<Zone>();
        if(zone != null) return zone;
        
        WaitingCursor waitingCursor = hit.transform.GetComponent<WaitingCursor>();
        if(waitingCursor != null) return waitingCursor.originalZone;
        
        return null;
    }

    private Vector2 GetWorldPosition(Vector3 touchPosition) {
        return Camera.main.ScreenToWorldPoint(touchPosition);
    }

    private Cursor CreateCursor(Zone zone, Touch touch) {
        GameObject circle = Instantiate(zone.circlePrefab);
        circle.transform.position = GetWorldPosition(touch.position);
        circle.name = "Touch " + touch.fingerId;

        Cursor cursor = circle.AddComponent<Cursor>();
        cursor.InitCursor(touch.fingerId, zone);
        cursors.Add(cursor);
        return cursor;
    }

    private void CreateBow(Zone zone, Cursor firstCursor, Touch newFinger) {
        Cursor secondCursor = CreateCursor(zone, newFinger);
        if(!zone.canShoot) 
            return;

        SetActivity(firstCursor, false);
        SetActivity(secondCursor, false);
        WaitingCursor waitingCursor = CreateWaitingCursor(zone, firstCursor, secondCursor);
        zone.bowRenderer.StartRendering(firstCursor.gameObject.transform,  secondCursor.gameObject.transform, waitingCursor.transform);
    }

    private WaitingCursor CreateWaitingCursor(Zone zone, Cursor firstCursor, Cursor secondCursor) {
        GameObject circle = Instantiate(zone.waitingCirclePrefab);
        circle.name = zone.gameObject.name + " Waiting Cursor";
        WaitingCursor waitingCursor = circle.GetComponent<WaitingCursor>();
        waitingCursor.Init(zone, firstCursor.gameObject, secondCursor.gameObject);
        waitingCursors.Add(waitingCursor);
        return waitingCursor;
    }

    private void DrawBow(Zone zone, Cursor firstCursor, Cursor secondCursor, Touch newFinger) {
        RaycastHit2D hit = Physics2D.Raycast(GetWorldPosition(newFinger.position), Vector2.zero, 0);
        if(hit.transform == null || hit.transform.GetComponent<WaitingCursor>() == null) return;
        Cursor thirdCursor = CreateCursor(zone, newFinger);
        thirdCursor.cursorType = CursorType.BULLET_CURSOR;
        DeleteWaitingCursor(zone);
        zone.bowRenderer.StartRendering(firstCursor.gameObject.transform,  secondCursor.gameObject.transform, thirdCursor.gameObject.transform);
    }

    private void SetActivity(Cursor cursor, bool activity) {
        cursor.gameObject.GetComponent<CircleCollider2D>().enabled = activity;
        cursor.gameObject.GetComponent<CircleCollider2D>().enabled = activity;
    }

    private void DeleteWaitingCursor(Zone zone) {
        WaitingCursor waitingCursor = waitingCursors.Find(cursor => cursor.originalZone == zone);
        if(waitingCursor == null) return;

        waitingCursors.Remove(waitingCursor);
        Destroy(waitingCursor.gameObject);
    }

    private IEnumerator EnableShoot(Zone zone) {
        yield return new WaitForSeconds(1);

        zone.canShoot = true;
        List<Cursor> zoneCursors = GetZoneCursors(zone);
        if(zoneCursors.Count <= 1) yield break;

        WaitingCursor waitingCursor = waitingCursors.Find(waitingCursor => waitingCursor.originalZone == zone);
        if(waitingCursor != null) yield break;


        Cursor firstCursor = zoneCursors[0];
        Cursor secondCursor = zoneCursors[1];
        waitingCursor = CreateWaitingCursor(zone, firstCursor, secondCursor);
        zone.bowRenderer.StartRendering(
            firstCursor.gameObject.transform,
            secondCursor.gameObject.transform,
            waitingCursor.transform
        );

        SetActivity(firstCursor, false);
        SetActivity(secondCursor, false);
    }

    /* TOUCH PHASES */
    private void BeginTouch(Touch touch) {
        Zone zone = GetZone(GetWorldPosition(touch.position));
        if(zone == null) return;

        List<Cursor> zoneCursors = GetZoneCursors(zone);
        switch(zoneCursors.Count) {
            case 0 :
                CreateCursor(zone, touch);
                break;
            case 1:
                CreateBow(zone, zoneCursors[0], touch);
                break;
            case 2:
                DrawBow(zone, zoneCursors[0], zoneCursors[1], touch);
                break;
        }
    }

    private void MoveCircle(Touch touch) {
        Cursor cursor = cursors.Find(cursor => cursor.fingerId == touch.fingerId);
        if(cursor == null) return;

        Zone zone = GetZone(GetWorldPosition(touch.position));

        GameObject cursorObject = cursor.gameObject;

        // Check if the fingers is in the correct zone
        if(zone == null || zone != cursor.originalZone) {
            cursorObject.SetActive(false);
            return;
        }

        if(!cursorObject.activeSelf) cursorObject.SetActive(true);
        cursorObject.transform.position = GetWorldPosition(touch.position);
    }

    private void RemoveCircle(Touch touchToRemove) {
        Cursor cursorToRemove = cursors.Find(cursor => cursor.fingerId == touchToRemove.fingerId);
        if(cursorToRemove == null) return;

        cursors.Remove(cursorToRemove);
        List<Cursor> zoneCursors = GetZoneCursors(cursorToRemove.originalZone);
        
        switch(zoneCursors.Count) {
            case 0:
                Destroy(cursorToRemove.gameObject);
                break;

            case 1:
                DeleteWaitingCursor(cursorToRemove.originalZone);
                SetActivity(GetZoneCursors(cursorToRemove.originalZone)[0], true);
                cursorToRemove.originalZone.bowRenderer.StopRendering();
                Destroy(cursorToRemove.gameObject);
                break;

            case 2:
                Cursor firstCursor = zoneCursors[0];
                Cursor secondCursor = zoneCursors[1];
                Zone zone = cursorToRemove.originalZone;
                
                if(cursorToRemove.cursorType == CursorType.BULLET_CURSOR) {
                    zone.bowRenderer.StopRendering();
                    SetActivity(firstCursor, true);
                    SetActivity(secondCursor, true);
                    SetActivity(cursorToRemove, true);

                    cursorToRemove.cursorType = CursorType.BULLET;
                    Bullet bullet = cursorToRemove.gameObject.AddComponent<Bullet>();
                    bullet.Shoot(
                        firstCursor.gameObject.transform.position,
                        secondCursor.gameObject.transform.position
                    );

                    zone.canShoot = false;
                    StartCoroutine(EnableShoot(zone));
                }

                else {
                    cursors.Remove(secondCursor);
                    CreateBow(zone, firstCursor, Input.GetTouch(secondCursor.fingerId));
                    Destroy(secondCursor.gameObject);
                }

                break;              
        }
    }
}
