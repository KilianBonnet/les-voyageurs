using UnityEngine;

public class OuterWheel : MonoBehaviour
{
    [SerializeField] float rotationSpeed = .2f;
    [SerializeField] float maxClickTime = .3f;

    private SelectionRenderer clickedCell;
    private bool isClicking;
    private float clickDuration;
    private Vector2 lastMousePosition;

    void Update()
    {
        if(isClicking)
            Clicking();
        
        else
            Idling();
    }

    private Vector2 getMouseWorldPosition() {
        return new Vector2 ( Camera.main.ScreenToWorldPoint (Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
    }

    private void Clicking(){
        clickDuration += Time.deltaTime;

        if(Input.GetMouseButtonUp(0)){
            if(clickDuration < maxClickTime)
                clickedCell.OnClick();

            isClicking = false;
            clickDuration = 0;
            return;
        }

        if(clickDuration > maxClickTime) {
            Vector2 wheelPosition = new Vector2(
                transform.position.x,
                transform.position.y
            );
            Vector2 mouseWorldPosition = getMouseWorldPosition();

            Vector2 centerClickOriginVector = lastMousePosition - wheelPosition;
            Vector2 centerMousePositionVector = mouseWorldPosition - wheelPosition;

            float angleInRadians = Mathf.Atan2(centerMousePositionVector.y, centerMousePositionVector.x) - Mathf.Atan2(centerClickOriginVector.y, centerClickOriginVector.x);
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

            Debug.Log(angleInDegrees);

            transform.Rotate(0, 0, angleInDegrees);
            lastMousePosition = mouseWorldPosition;
        }
    }

    private void Idling(){
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        if(!Input.GetMouseButtonDown(0))
            return;

        lastMousePosition = getMouseWorldPosition();
        RaycastHit2D hit = Physics2D.Raycast(lastMousePosition, Vector2.zero, 0);

        if(hit.transform == null)
            return;

        Debug.Log(hit.transform.position);

        SelectionRenderer selectionRenderer = hit.transform.GetComponent<SelectionRenderer>();
        if(selectionRenderer == null) 
            return;

        clickedCell = selectionRenderer;
        isClicking = true;
    }
}
