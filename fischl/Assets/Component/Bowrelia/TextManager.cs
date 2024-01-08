using System.Collections;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] GameObject floatingText;

    private void OnEnable()
    {
        Slime.OnDeath += HandleDeath;
    }

    private void HandleDeath(Transform slimeTransform, Cursor cursor)
    {
        Vector3 rotationEuler = Vector3.zero;
        if (floatingText)
        {
            switch (cursor.originalZone.name)
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
            Instantiate(floatingText, slimeTransform.position, rotation);
        }
    }
}

