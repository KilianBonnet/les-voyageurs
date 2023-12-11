using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleManager : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public GameObject correctSpot;
    
    private GameObject selectedObject;
    private Vector3 originalPosition;
    private GameObject droppingArea;
    private GameObject[] answersSpots;
    private string[] answerSpotStrings;

    public void Start()
    {
        answersSpots = GameObject.FindGameObjectsWithTag("Placeholder");
        answerSpotStrings = answersSpots[0].GetComponentInChildren<TMPro.TextMeshProUGUI>().text.Split(' ');
        droppingArea = correctSpot.transform.parent.gameObject;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        selectedObject = eventData.pointerCurrentRaycast.gameObject;
        originalPosition = selectedObject.transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData){}

    public void OnDrag(PointerEventData eventData)
    {
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

    private float[] GetCoordinates(GameObject theObject){
        //retrieve the object coordinates according to world space
        Vector3 objectLeftBottom = theObject.transform.TransformPoint(theObject.GetComponent<RectTransform>().rect.min);
        Vector3 objectRightTop = theObject.transform.TransformPoint(theObject.GetComponent<RectTransform>().rect.max);

        //retrieve the object coordinates according to screen space
        Vector3 objectLeftBottomScreen = Camera.main.WorldToScreenPoint(objectLeftBottom);
        Vector3 objectRightTopScreen = Camera.main.WorldToScreenPoint(objectRightTop);

        //retrieve the object coordinates according to screen space
        float objectLeft = objectLeftBottomScreen.x;
        float objectBottom = objectLeftBottomScreen.y;
        float objectRight = objectRightTopScreen.x;
        float objectTop = objectRightTopScreen.y;

        return new float[] {objectLeft, objectBottom, objectRight, objectTop};
    }

    public void OnDrop(PointerEventData eventData)
    {
        float[] droppingAreaCoordinates = GetCoordinates(droppingArea);
        //retrieve the droppingarea coordinates according to screen space
        float droppingAreaLeft = droppingAreaCoordinates[0];
        float droppingAreaBottom = droppingAreaCoordinates[1];
        float droppingAreaRight = droppingAreaCoordinates[2];
        float droppingAreaTop = droppingAreaCoordinates[3];

        //if the selectedObject is not within the limits or outside the screen area
        if (eventData.position.x < droppingAreaLeft || eventData.position.x > droppingAreaRight || eventData.position.y < droppingAreaBottom || eventData.position.y > droppingAreaTop)
        {
            selectedObject.transform.position = originalPosition;
        } else {
            selectedObject.transform.position = eventData.position;
            //update original position
            originalPosition = eventData.position;
        }
        
        float[] correctSpotCoordinates = GetCoordinates(correctSpot);

        //retrieve the correctSpot coordinates according to screen space
        float correctSpotLeft = correctSpotCoordinates[0];
        float correctSpotBottom = correctSpotCoordinates[1];
        float correctSpotRight = correctSpotCoordinates[2];
        float correctSpotTop = correctSpotCoordinates[3];

        //retrieve the correctSpot's middle coordinates
        Vector3 correctSpotMiddle = correctSpot.transform.TransformPoint(correctSpot.GetComponent<RectTransform>().rect.center);
        Vector3 correctSpotMiddleScreen = Camera.main.WorldToScreenPoint(correctSpotMiddle);

        float offsetX = 0.5f;
        float offsetY = 0.5f;

        //if the selectedobject is within the limits of the correctSpot area
        if (eventData.position.x > correctSpotLeft + offsetX && eventData.position.x < correctSpotRight - offsetX && eventData.position.y > correctSpotBottom + offsetY && eventData.position.y < correctSpotTop - offsetY)
        {
            //replace it correctly in the middle of correctspot
            selectedObject.transform.position = correctSpotMiddleScreen;
            selectedObject.GetComponent<PuzzleManager>().enabled = false;
            int answerSpotIndex = int.Parse(correctSpot.name);
            foreach (GameObject answerSpot in answersSpots)
            {
                //replace the corresponding [answer] with the selectedObject's text
                string[] allSpots = answerSpot.GetComponentInChildren<TMPro.TextMeshProUGUI>().text.Split(']');
                Debug.Log(allSpots[answerSpotIndex]);



                /*string[] allSpots = answerSpot.GetComponentInChildren<TMPro.TextMeshProUGUI>().text.Split(']');
                allSpots[answerSpotIndex] = selectedObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text;
                answerSpot.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = string.Join("] [", allSpots);*/
            }
        }
    }
}