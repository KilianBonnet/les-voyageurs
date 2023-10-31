using UnityEngine;

public class OuterWheel : MonoBehaviour
{
    [SerializeField] float rotationSpeed = .2f;

    private SelectionRenderer clickedCell;
    private bool isClicking;
    private float clickDuration;

    void Update()
    {
        if(isClicking)
            Clicking();
        
        else
            Idling();
    }

    private void Clicking(){
        clickDuration += Time.deltaTime;
        if(Input.GetMouseButtonUp(0)){
            Debug.Log(clickDuration);
            if(clickDuration < .1)
                clickedCell.OnClick();

            isClicking = false;
            clickDuration = 0;
            return;
        }
    }

    private void Idling(){
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        if(!Input.GetMouseButtonDown(0))
            return;

        RaycastHit2D hit= Physics2D.Raycast(
            new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint (Input.mousePosition).y), Vector2.zero, 0
        );

        if(hit.transform == null)
            return;

        SelectionRenderer selectionRenderer = hit.transform.GetComponent<SelectionRenderer>();
        if(selectionRenderer == null) 
            return;

        clickedCell = selectionRenderer;
        isClicking = true;
    }
}
