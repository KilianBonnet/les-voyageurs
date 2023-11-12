using TMPro;
using UnityEngine;

public class ElementContainer : MonoBehaviour
{
    [SerializeField] private GameObject elementPrefab;
    [SerializeField] private TextMeshProUGUI countUi;
    private int count = 1;

    private Element draggedElement;

    private int touchId;
    

    void Start()
    {
        touchId = -1;
        countUi.text = count.ToString();
    }

    public void IncreaseCount(int amount) {
        count += amount;
    }

    private void Update()
    {
        for (int i = 0; i <  Input.touchCount; i++) {
            Touch touch = Input.GetTouch(i);

            switch (touch.phase) {
                case TouchPhase.Began:
                    OnTouchDown(touch);
                    break;

                case TouchPhase.Moved:
                    if(touch.fingerId != touchId) break;
                    draggedElement.transform.position = GetWorldPosition(touch.position);
                    break;
                
                case TouchPhase.Ended:
                    OnTouchExit(touch);
                    break;
            }
        }
    }

    private void OnTouchDown(Touch touch) {
        Vector2 touchPosition = GetWorldPosition(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero, 0);
        if(hit.transform == null || hit.transform.gameObject != transform.gameObject)
            return;

        if(touchId != -1 || count <= 0) 
            return;

        count--;
        countUi.text = count.ToString();

        touchId = touch.fingerId;
        draggedElement = Instantiate(elementPrefab).GetComponent<Element>();
        draggedElement.transform.position = touchPosition;
    }

    private void OnTouchExit(Touch touch) {
        if(touch.fingerId != touchId) return;
        touchId = -1;

        Vector2 touchPosition = GetWorldPosition(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero, 0);

        if(hit.transform == null || !hit.transform.CompareTag("Wheel")) draggedElement.OnDropped();
        else {
            count++;
            countUi.text = count.ToString();
            Destroy(draggedElement.gameObject);
        }
    }
    
    private Vector2 GetWorldPosition(Vector3 touchPosition) {
        return Camera.main.ScreenToWorldPoint(touchPosition);
    }


}
