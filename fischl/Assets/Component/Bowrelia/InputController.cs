using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private GameObject redTouch;
    [SerializeField] private GameObject blueTouch;
    [SerializeField] private GameObject greenTouch;

    private Dictionary<int, GameObject> touchObjects = new Dictionary<int, GameObject>();

    private void Update() {
        for (int i = 0; i <  Input.touchCount; i++) {
            Touch touch = Input.GetTouch(i);

            switch (touch.phase) {
                case TouchPhase.Began:
                    CreateCircle(touch);
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

    private Vector2 GetWorldPosition(Vector3 touchPosition) {
        return Camera.main.ScreenToWorldPoint(touchPosition);
    }

    private bool IsTouchingZone(Vector3 touchWorldPosition) {
        RaycastHit2D hit = Physics2D.Raycast(touchWorldPosition, Vector2.zero, 0);
        return hit.transform != null && hit.transform.gameObject == this;
    }

    private void CreateCircle(Touch touch) {
        GameObject circle = Instantiate(redTouch);
        circle.name = "Touch " + touch.fingerId;
        circle.transform.position = GetWorldPosition(touch.position);

        touchObjects.Add(touch.fingerId, circle);
    }

    private void MoveCircle(Touch touch) {
        GameObject circle = touchObjects[touch.fingerId];
        circle.transform.position = GetWorldPosition(touch.position);
    }

    private void RemoveCircle(Touch touch) {
        GameObject circle = touchObjects[touch.fingerId];
        Destroy(circle);
        touchObjects.Remove(touch.fingerId);
    }
}
