using UnityEngine;

public class InnerWheel : MonoBehaviour
{
    [SerializeField] private Transform wheelAnchor;
    private Vector3 lastPosition;
    private Vector3 offset;

    private bool needSetup;
    private bool isClicking;


    // Update is called once per frame
    void Update()
    {
        CheckClick();

        if(isClicking) 
            Clicking();
        else 
            needSetup = true;
    }

    private Vector3 GetMousePosition() {
        return new Vector3 (
                Camera.main.ScreenToWorldPoint (Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint (Input.mousePosition).y,
                0
            );
    }

    private void CheckClick() {
        if(Input.GetMouseButtonUp(0)) {
            isClicking = false;
            return;
        }
            
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(GetMousePosition(), Vector2.zero, 0);
            if(hit.transform != null && hit.transform.CompareTag("InnerWheel")) 
                isClicking = true;
        }
    }

    void Clicking() {
        Vector3 mousePosition = GetMousePosition();
        if(!needSetup) {
            wheelAnchor.Translate(mousePosition - wheelAnchor.position + offset);
            lastPosition = wheelAnchor.position;
        }
        else {
            offset = wheelAnchor.position - mousePosition;
            needSetup = false;
        }
    }
}
