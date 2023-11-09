using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubePuzzleElement : MonoBehaviour
{
   /*private GameObject selectedObject;
    public Vector3 originalPosition;
    public GameObject droppingArea;
    public GameObject correctSpot;
    public GameObject[] spotTwinnies;*/

    private Vector3 offset;
    private bool isDragging = false;
    private GameObject draggedObject;
    private Vector3 initialPosition;
    public GameObject correspondingObject;

    public void Start()
    { 
      /*  selectedObject = gameObject;
        originalPosition = selectedObject.transform.position;*/
       
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            draggedObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        draggedObject = gameObject;
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        initialPosition = gameObject.transform.position;
    }

    //TODO prevent player from switching blocs between 2 player areas
    void OnMouseUp()
    {
        isDragging = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(draggedObject.transform.position, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != draggedObject)
            {
                Vector3 tempPos = collider.gameObject.transform.position;
                collider.gameObject.transform.position = initialPosition;
                draggedObject.transform.position = tempPos;

                GameObject otherCorrespondingObject = collider.gameObject.GetComponent<CubePuzzleElement>().correspondingObject;
                Vector3 vect1 = otherCorrespondingObject.transform.position;
                Vector3 vect2 = correspondingObject.transform.position;
                Vector3 tmp = vect2;
                correspondingObject.transform.position = vect1;
                otherCorrespondingObject.transform.position = tmp;
                break;
            }
            else
            {
                draggedObject.transform.position = initialPosition;
            }
        }
    }
}