using System.Collections.Generic;
using UnityEngine;

public class MultipleTouchPodRythme : MonoBehaviour
{
    private Dictionary<int, ObjectVisibilityController> singleTouchesDict = new Dictionary<int, ObjectVisibilityController>();
    private Dictionary<int, DoubleCircleTap> doubleTouchDict = new Dictionary<int, DoubleCircleTap>();


    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            if (t.phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(GetWorldPosition(t.position), Vector2.zero, 0);
                if (hit.transform == null) return;

                ObjectVisibilityController circleToTap = hit.transform.GetComponent<ObjectVisibilityController>();
                DoubleCircleTap doubleCircleToTap = hit.transform.GetComponent<DoubleCircleTap>();

                if (circleToTap == null)
                {
                    if (doubleCircleToTap == null) return;
                    else
                    {
                        doubleCircleToTap.OnTouchStart();
                        doubleTouchDict.Add(t.fingerId, doubleCircleToTap);
                        return;
                    }
                }
                circleToTap.OnTouchStart();

                singleTouchesDict.Add(t.fingerId, circleToTap);
            }
            else if (t.phase == TouchPhase.Ended)
            {
                if (!singleTouchesDict.ContainsKey(t.fingerId)) {
                    if (!doubleTouchDict.ContainsKey(t.fingerId)) return;
                    else doubleTouchDict.Remove(t.fingerId);
                }
                singleTouchesDict.Remove(t.fingerId);
            }
/*            else if (t.phase == TouchPhase.Moved){ }*/
            ++i;
        }
    }

    private Vector2 GetWorldPosition(Vector3 touchPosition)
    {
        return Camera.main.ScreenToWorldPoint(touchPosition);
    }
}