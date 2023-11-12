using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorMixing : MonoBehaviour
{
    public List<GameObject> colorObjects; // List of colored rectangles
    public GameObject drawArea; // The area where you draw
    public GameObject drawingCirclePrefab; // Prefab of the drawing circle
    private Transform targetCircle;

    private List<Transform> placedCircles = new List<Transform>();
    private Color currentColor;
    private void Start()
    {
        targetCircle = Instantiate(drawingCirclePrefab).transform;
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
                    CheckForCollisions();
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
        placedCircles.Add(drawingCircle.transform);
        // Optionally, you can adjust the scale of the drawing circle if needed.
        // drawingCircle.transform.localScale = new Vector3(fixedBrushSize, fixedBrushSize, 1.0f);
    }

    private void CheckForCollisions()
    {
        int lastIndex;
        if (placedCircles == new List<Transform>())
        {
            lastIndex = 0;
        }
        else
        {
            lastIndex = placedCircles.Count - 1;
        }
        Transform newCircle = placedCircles[lastIndex];

        for (int i = 0; i < lastIndex; i++)
        {
            Transform existingCircle = placedCircles[i];

            if (IsColliding(newCircle, existingCircle))
            {
                Color newCircleColor = newCircle.GetComponent<SpriteRenderer>().color;
                Color existingCircleColor = existingCircle.GetComponent<SpriteRenderer>().color;

                if (newCircleColor != existingCircleColor)
                {
                    // Calculate the average color between the two circles
                    Color averageColor = CalculateAverageColor(newCircleColor, existingCircleColor);
                    Debug.Log("Collision detected! Average Color: " + averageColor);
                    currentColor = averageColor;
                }
            }
        }
    }

    private bool IsColliding(Transform circle1, Transform circle2)
    {
        // Calculate the distance between the centers of the two circles
        float distance = Vector2.Distance(circle1.position, circle2.position);

        // Assuming both circles have the same radius
        float radius = circle1.GetComponent<CircleCollider2D>().radius;

        // Check if the distance is less than the sum of their radii
        return distance < radius * 2;
    }

    private Color CalculateAverageColor(Color color1, Color color2)
    {
        // Calculate the average color by taking the average of each color channel
        float r = (color1.r + color2.r) / 2f;
        float g = (color1.g + color2.g) / 2f;
        float b = (color1.b + color2.b) / 2f;

        return new Color(r, g, b);
    }
}
