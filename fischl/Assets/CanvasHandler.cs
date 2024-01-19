using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    public void EndGameCanvas()
    {
        HideGameUI();
    }

    private void HideGameUI()
    {
        GameObject.Find("Progress").SetActive(false);
        GameObject.Find("Bonus Notification").SetActive(false);
        GameObject.Find("VRMap").SetActive(false);
    }
}
