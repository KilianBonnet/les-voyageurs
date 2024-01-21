using System.Collections;
using UnityEngine;
using System;

public class VrMap : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    private GameObject touched;
    public static event Action<Transform, int> EnemyEntered;

    private void Start() {
        touched = GameObject.Find("Touched");
        touched.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.transform.CompareTag("Enemy")) {
            int score = other.transform.gameObject.GetComponent<Slime>().score;
            EnemyEntered.Invoke(other.transform,score);
            Destroy(other.gameObject);
            scoreManager.IncreaseScore(-score);
            StartCoroutine(ShowAndHideObject(touched, 3f));
        }
    }

    private IEnumerator ShowAndHideObject(GameObject obj, float duration)
    {
        float timePassed = 0f;
        while (timePassed <= duration)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(0.125f);
            obj.SetActive(false);
            yield return new WaitForSeconds(0.125f);
            timePassed += 1.0f;
        }
        obj.SetActive(false);
    }
}
