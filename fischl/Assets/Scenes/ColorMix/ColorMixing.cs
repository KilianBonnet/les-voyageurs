using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorMixing : MonoBehaviour
{
    public List<GameObject> colorObjects; // List of colored rectangles
    public GameObject drawArea; // The area where you draw
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
                    // Draw on the canvas
                    Renderer renderer = drawArea.GetComponent<SpriteRenderer>();
                    Material material = renderer.material;
                    material.color = currentColor;
                }

            }
        }
    }

    private void ApplyBrush(Texture2D texture, int x, int y)
    {
        for (int i = x - (int)fixedBrushSize; i <= x + (int)fixedBrushSize; i++)
        {
            for (int j = y - (int)fixedBrushSize; j <= y + (int)fixedBrushSize; j++)
            {
                if (i >= 0 && i < texture.width && j >= 0 && j < texture.height)
                {
                    texture.SetPixel(i, j, currentColor);
                }
            }
        }

        texture.Apply();
    }

    public void SetCurrentColor(Color color)
    {
        currentColor = color;
    }
}
