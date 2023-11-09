using UnityEngine;

public class BowRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private bool isRendering;
    private Transform firstPoint;
    private Transform secondPoint;
    private Transform thirdPoint;

    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void StartRendering(Transform firstPoint, Transform secondPoint, Transform thirdPoint = null) {
        isRendering = true;

        this.firstPoint = firstPoint;
        this.secondPoint = secondPoint;
        this.thirdPoint = thirdPoint;
    }

    public void StopRendering() {
        isRendering = false;
        lineRenderer.positionCount = 0;
    }

    private void Update() {
        if(!isRendering) return;

        Vector3[] newPositions;
        if(thirdPoint != null) {
            lineRenderer.positionCount = 3;
            Vector3[] cursorPositions = { firstPoint.position, thirdPoint.position, secondPoint.position };
            newPositions = cursorPositions;
        }
        else {
            lineRenderer.positionCount = 2;
            Vector3[] cursorPositions = { firstPoint.position, secondPoint.position };
            newPositions = cursorPositions;
        }

        lineRenderer.SetPositions(newPositions);
    }

}
