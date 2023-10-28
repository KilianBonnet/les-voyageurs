using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleManager : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public GameObject selectedObject;
    public Vector3 originalPosition;
    public GameObject droppingArea;
    public GameObject correctSpot;
    public GameObject[] spotTwinnies;

    public void Start()
    {
        originalPosition = selectedObject.transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        selectedObject = eventData.pointerCurrentRaycast.gameObject;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("dragging!!");
        if (eventData.position.x > 0 && eventData.position.x < Screen.width && eventData.position.y > 0 && eventData.position.y < Screen.height)
        {
            selectedObject.transform.position = eventData.position;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.position.x < 0 && eventData.position.x > Screen.width && eventData.position.y < 0 && eventData.position.y > Screen.height)
        {
            selectedObject.transform.position = originalPosition;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //retrieve the droppingarea coordinates according to world space
        Vector3 droppingAreaLeftBottom = droppingArea.transform.TransformPoint(droppingArea.GetComponent<RectTransform>().rect.min);
        Vector3 droppingAreaRightTop = droppingArea.transform.TransformPoint(droppingArea.GetComponent<RectTransform>().rect.max);

        //retrieve the droppingarea coordinates according to screen space
        Vector3 droppingAreaLeftBottomScreen = Camera.main.WorldToScreenPoint(droppingAreaLeftBottom);
        Vector3 droppingAreaRightTopScreen = Camera.main.WorldToScreenPoint(droppingAreaRightTop);

        //retrieve the droppingarea coordinates according to screen space
        float droppingAreaLeft = droppingAreaLeftBottomScreen.x;
        float droppingAreaBottom = droppingAreaLeftBottomScreen.y;
        float droppingAreaRight = droppingAreaRightTopScreen.x;
        float droppingAreaTop = droppingAreaRightTopScreen.y;

        //if the selectedObjetc is not within the limits or outside the screen area
        if (eventData.position.x < droppingAreaLeft || eventData.position.x > droppingAreaRight || eventData.position.y < droppingAreaBottom || eventData.position.y > droppingAreaTop)
        {
            selectedObject.transform.position = originalPosition;
        }
        else
        {
            selectedObject.transform.position = eventData.position;
            //update original position
            originalPosition = eventData.position;
        }
         
        //retrieve the correctSpot coordinates according to world space
        Vector3 correctSpotLeftBottom = correctSpot.transform.TransformPoint(correctSpot.GetComponent<RectTransform>().rect.min);
        Vector3 correctSpotRightTop = correctSpot.transform.TransformPoint(correctSpot.GetComponent<RectTransform>().rect.max);

        //retrieve the correctSpot coordinates according to screen space
        Vector3 correctSpotLeftBottomScreen = Camera.main.WorldToScreenPoint(correctSpotLeftBottom);
        Vector3 correctSpotRightTopScreen = Camera.main.WorldToScreenPoint(correctSpotRightTop);

        //retrieve the correctSpot coordinates according to screen space
        float correctSpotLeft = correctSpotLeftBottomScreen.x;
        float correctSpotBottom = correctSpotLeftBottomScreen.y;
        float correctSpotRight = correctSpotRightTopScreen.x;
        float correctSpotTop = correctSpotRightTopScreen.y;

        //retrieve the correctSpot's middle coordinates
        Vector3 correctSpotMiddle = correctSpot.transform.TransformPoint(correctSpot.GetComponent<RectTransform>().rect.center);
        Vector3 correctSpotMiddleScreen = Camera.main.WorldToScreenPoint(correctSpotMiddle);

        float offsetX = 0.5f;
        float offsetY = 0.5f;

        //if the selectedobject is within the limits of the correctSpot turn th spot green
        if (eventData.position.x > correctSpotLeft + offsetX && eventData.position.x < correctSpotRight - offsetX && eventData.position.y > correctSpotBottom + offsetY && eventData.position.y < correctSpotTop - offsetY)
        {
            correctSpot.GetComponent<SpriteRenderer>().color = Color.green;
            //replace it correctly in the middle of correctspot
            selectedObject.transform.position = correctSpotMiddleScreen;
            //remove the drag events management from the selectedObject
            selectedObject.GetComponent<PuzzleManager>().enabled = false;
            foreach (GameObject spotTwinny in spotTwinnies)
            {
                //TODO: clone the selectedObject
                
                spotTwinny.GetComponent<SpriteRenderer>().color = Color.green;
            }

        }


    }
}