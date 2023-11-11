using System.Collections.Generic;
using UnityEngine;

public class SelectionWheel : MonoBehaviour
{
    [SerializeField] private bool resetPositionOnMouseExit;
    [SerializeField] private float distanceFromFinger = 2f;

    private Vector2 initialPosition;
    private List<int> activeTouchIDs = new List<int>();
    private Vector2 lastSecondTouchPosition;

    private void Start() {
        initialPosition = transform.position;
        SetChildrenActive(false);
    }

    private void Update()
    {
        TouchHandler();
    }

    private void TouchHandler() {
        for (int i = 0; i <  Input.touchCount; i++) {
            Touch touch = Input.GetTouch(i);

            switch (touch.phase) {
                case TouchPhase.Began:
                    OnTouchDown(touch);
                    break;

                case TouchPhase.Moved:
                    OnTouchDrag(touch);
                    break;
                
                case TouchPhase.Ended:
                    OnTouchExit(touch);
                    break;
            }
        }
    }

    private void OnTouchDown(Touch touch) {
        RaycastHit2D hit = Physics2D.Raycast(GetWorldPosition(touch.position), Vector2.zero, 0);

        if(hit.transform == null || hit.transform.gameObject != gameObject)
            return;

        activeTouchIDs.Add(touch.fingerId);

        switch(activeTouchIDs.Count) {
            case 1:
                SetChildrenActive(true);
                break;

            case 2:
                lastSecondTouchPosition = GetWorldPosition(touch.position);
                break;
        }
    }

    private void OnTouchDrag(Touch touch) {
        if(activeTouchIDs.Count == 0)
            return;

        if(touch.fingerId == activeTouchIDs[0])
            transform.position = GetWorldPosition(touch.position);

        if(activeTouchIDs.Count == 2) {
            Vector2 wheelPosition = transform.position;
            Vector2 touchWorldPosition = GetWorldPosition(touch.position);

            Vector2 centerClickOriginVector = lastSecondTouchPosition - wheelPosition;
            Vector2 centerMousePositionVector = touchWorldPosition - wheelPosition;

            float angleInRadians = Mathf.Atan2(centerMousePositionVector.y, centerMousePositionVector.x) - Mathf.Atan2(centerClickOriginVector.y, centerClickOriginVector.x);
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

            transform.Rotate(0, 0, angleInDegrees);
            lastSecondTouchPosition = touchWorldPosition;
        }
    }

    private void OnTouchExit(Touch touch) {
        if(!activeTouchIDs.Contains(touch.fingerId))
            return;

        activeTouchIDs.Remove(touch.fingerId);

        if(activeTouchIDs.Count > 0)
            return;

        SetChildrenActive(false);
        
        if(resetPositionOnMouseExit)
            transform.position = initialPosition;
    }

private void SetChildrenActive(bool state) {
    int nbSections = transform.childCount;
    float deltaAngle = 360f / nbSections;

    for (int i = 0; i < nbSections; i++) {
        Transform section = transform.GetChild(i);
        section.gameObject.SetActive(state);

        // Calculer l'angle en radians
        float angle = i * deltaAngle * Mathf.Deg2Rad;

        // Calculer les coordonnées polaires
        float x = Mathf.Cos(angle) * distanceFromFinger;
        float y = Mathf.Sin(angle) * distanceFromFinger;

        section.localPosition = new Vector3(x, y, 0f);
        section.localRotation = Quaternion.Euler(0f, 0f, 90 + deltaAngle * i);
    }
}

    private Vector2 GetWorldPosition(Vector3 touchPosition) {
        return Camera.main.ScreenToWorldPoint(touchPosition);
    }

}
