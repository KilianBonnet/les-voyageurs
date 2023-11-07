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

    public void StartRendering(Transform firstPoint, Transform secondPoint, Transform thirdPoint) {
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
        lineRenderer.positionCount = 3;
        Vector3[] newPositions  = { firstPoint.position, thirdPoint.position, secondPoint.position };
        lineRenderer.SetPositions(newPositions);
    }

}
