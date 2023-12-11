using System.Collections;
using UnityEngine;

public class VrMap : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    private GameObject touched = GameObject.Find("Touched");


    private void OnTriggerEnter2D(Collider2D other) {

        if(other.transform.CompareTag("Enemy")) {
            Destroy(other.gameObject);
            scoreManager.IncreaseScore(-200);
            ShowAndHideObject(touched, 3.0f);
        }
    }

    private IEnumerator ShowAndHideObject(GameObject obj, float duration)
    {
        float timePassed = 0f;
        while (timePassed < duration)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            obj.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            timePassed += 1.0f;
        }
        obj.SetActive(false);
    }
}
