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
        this.firstPoint = firstPoint;
        this.secondPoint = secondPoint;
        this.thirdPoint = thirdPoint;
    }

}
