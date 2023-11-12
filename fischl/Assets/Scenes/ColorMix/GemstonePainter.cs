using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GemstonePainter : MonoBehaviour
{
    private ColorMixing canvas;
    public TextMeshProUGUI finishText;
    private void OnMouseDown()
    {
        // Find the ColorMixing object in the scene
        canvas = FindObjectOfType<ColorMixing>();

        // Change the color of the object to the selected color
        Renderer renderer = GetComponent<Renderer>();
        Color color = canvas.getSelectedColor();
        if (renderer != null && color != null)
        {
            renderer.material.color = color;
            ToggleFinishTextVisibility();
        }
    }

    private void ToggleFinishTextVisibility()
    {
        if (finishText != null)
        {
            finishText.gameObject.SetActive(true);
        }
    }

}
