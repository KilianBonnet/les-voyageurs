using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] GameObject floatingText;

    private void OnEnable()
    {
        Slime.OnDeath += HandleDeath;
        VrMap.EnemyEntered += HandleMapEntered;
    }

    private void HandleDeath(Transform slimeTransform, Cursor cursor, int score)
    {
        floatingText.GetComponent<TextMesh>().text = "+"+ $"{score} pts";

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

    private void HandleMapEntered(Transform slimeTransform)
    {
            floatingText.GetComponent<TextMesh>().text = "-125 pts";
            var rotation = Quaternion.Euler(Vector3.zero);
            Instantiate(floatingText, slimeTransform.position, rotation);
        
    }
}

