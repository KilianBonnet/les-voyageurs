using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OuterWheel : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = .2f;
    [SerializeField] private float maxClickTime = .3f;

    [SerializeField] AudioSource clickAudio;
    [SerializeField] AudioSource unClickAudio;

    [SerializeField] AudioSource errorAudio;
    private bool needReset;

    private int selectedElements;
    [SerializeField] private TextMeshProUGUI topText;
    [SerializeField] private TextMeshProUGUI bottomText;

    private SelectionRenderer clickedCell;
    private bool isClicking;
    private float clickDuration;
    private Vector2 lastMousePosition;

    void Update()
    {
        if(errorAudio.isPlaying)
            return;

        if(needReset) {
            needReset = false;
            topText.text = "0 / 3";
            bottomText.text = "0 / 3";
        }

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
            if(clickDuration < maxClickTime) {
                int selection = clickedCell.OnClick();
                selectedElements += selection;

                if(selection < 0) unClickAudio.Play();
                else clickAudio.Play();

                string res;
                if(selectedElements < 3) {
                    res = selectedElements + " / 3";
                }
                else {
                    if(GameObject.Find("Area 0").GetComponent<SpriteRenderer>().sprite.name == "Desert_S")
                        SceneManager.LoadScene("Assemble!");

                    selectedElements = 0;
                    res = "Incorrect";
                    errorAudio.Play();
                    needReset = true;
                    SelectionRenderer.ResetEvent.Invoke();
                }

                topText.text = res;
                bottomText.text = res;
            }
                
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
}
