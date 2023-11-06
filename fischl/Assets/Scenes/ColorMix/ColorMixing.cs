using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorMixing : MonoBehaviour
{
    public List<GameObject> colorObjects; // List of colored rectangles
    public GameObject drawArea; // The area where you draw
    public GameObject drawingCirclePrefab; // Prefab of the drawing circle
    public float fixedBrushSize = 10f; // Define a fixed brush size

    private Color currentColor;

    private void Start()
    {
        // Initialize with a default color
        currentColor = Color.white;

        // Attach click events for color selection
        foreach (GameObject colorObject in colorObjects)
        {
            Color color = colorObject.GetComponent<SpriteRenderer>().color;
            colorObject.GetComponent<ClickableColor>().SetColor(color, this);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // Do not draw on UI elements
                return;
            }

            // Draw on the canvas
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == drawArea)
                {
                    // Create a drawing circle at the click position
                    CreateDrawingCircle(hit.point);
                }
            }
        }
    }

    public void SetCurrentColor(Color color)
    {
        currentColor = color;
    }

    private void CreateDrawingCircle(Vector3 position)
    {
        // Instantiate a drawing circle at the given position
        GameObject drawingCircle = Instantiate(drawingCirclePrefab, position, Quaternion.identity);

        // Set the color of the drawing circle
        SpriteRenderer circleRenderer = drawingCircle.GetComponent<SpriteRenderer>();
        circleRenderer.color = currentColor;

        // Optionally, you can adjust the scale of the drawing circle if needed.
        // drawingCircle.transform.localScale = new Vector3(fixedBrushSize, fixedBrushSize, 1.0f);
    }
}
