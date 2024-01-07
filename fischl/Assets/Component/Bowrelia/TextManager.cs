using System.Collections;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] GameObject floatingText;

    [SerializeField] GameObject redZone;
    [SerializeField] GameObject blueZone;
    [SerializeField] GameObject greenZone;

    private void OnEnable()
    {
        Slime.OnDeathText += HandleDeathText;
    }

    private void HandleDeathText(Vector3 slimeTransform, string zoneName)
    {
        Vector3 rotationEuler = Vector3.zero;
        if (floatingText)
        {
            switch (zoneName)
            {
                case "Red Zone":
                    break;
                case "Blue Zone":
                    rotationEuler = new Vector3(0, 0, 90);
                    break;
                case "Green Zone":
                    rotationEuler = new Vector3(0, 0, 270);
                    break;
                default:
                    break;
            }
            var rotation = Quaternion.Euler(rotationEuler); 
            Instantiate(floatingText, slimeTransform, rotation);
        }
    }
}

