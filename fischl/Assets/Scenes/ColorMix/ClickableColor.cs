using UnityEngine;
using System.Collections;

public class ClickableColor : MonoBehaviour
{
    private ColorMixing colorMixingCanvas;
    private Color color;

    public void SetColor(Color col, ColorMixing canvas)
    {
        color = col;
        colorMixingCanvas = canvas;
    }

    private void OnMouseDown()
    {
        colorMixingCanvas.SetCurrentColor(color);
        Debug.Log(color);
    }
}