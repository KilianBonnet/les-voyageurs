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

    private Vector2 GetMouseWorldPosition() {
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
            Vector2 mouseWorldPosition = GetMouseWorldPosition();

            Vector2 centerClickOriginVector = lastMousePosition - wheelPosition;
            Vector2 centerMousePositionVector = mouseWorldPosition - wheelPosition;

            float angleInRadians = Mathf.Atan2(centerMousePositionVector.y, centerMousePositionVector.x) - Mathf.Atan2(centerClickOriginVector.y, centerClickOriginVector.x);
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

            transform.Rotate(0, 0, angleInDegrees);
            lastMousePosition = mouseWorldPosition;
        }
    }

    private void Idling(){
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        if(!Input.GetMouseButtonDown(0))
            return;

        lastMousePosition = GetMouseWorldPosition();
        RaycastHit2D hit = Physics2D.Raycast(lastMousePosition, Vector2.zero, 0);

        if(hit.transform == null || !hit.transform.CompareTag("OuterWheel"))
            return;

        SelectionRenderer selectionRenderer = hit.transform.GetComponent<SelectionRenderer>();
        clickedCell = selectionRenderer;
        isClicking = true;
    }

    private void CheckClick() {
        if(Input.GetMouseButtonUp(0)) {
            isClicking = false;
            return;
        }
    }
}
