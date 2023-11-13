using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multipleTouch : MonoBehaviour
{
    private Dictionary<int, CubePuzzleElement> touchTileDict = new Dictionary<int, CubePuzzleElement>();

    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            if(t.phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(GetWorldPosition(t.position), Vector2.zero, 0);
                if (hit.transform == null) return;
               
                CubePuzzleElement cubePuzzleElement = hit.transform.GetComponent<CubePuzzleElement>();
                if (cubePuzzleElement == null) return;
                if (cubePuzzleElement.getCurrentlyTouch()) return;

                cubePuzzleElement.OnTouchStart();

                touchTileDict.Add(t.fingerId, cubePuzzleElement);
            }
            else if(t.phase == TouchPhase.Ended)
            {
                if (!touchTileDict.ContainsKey(t.fingerId)) return;

                CubePuzzleElement cubePuzzleElement = touchTileDict[t.fingerId];
                cubePuzzleElement.OnTouchEnd();
                touchTileDict.Remove(t.fingerId);
                            
            }        
            else if (t.phase == TouchPhase.Moved)
            {
                if (!touchTileDict.ContainsKey(t.fingerId)) return;
                CubePuzzleElement cubePuzzleElement = touchTileDict[t.fingerId];
                cubePuzzleElement.transform.position = GetWorldPosition(t.position);
            }
            ++i;
        }
    }

    private Vector2 GetWorldPosition(Vector3 touchPosition)
    {
        return Camera.main.ScreenToWorldPoint(touchPosition);
    }
}
