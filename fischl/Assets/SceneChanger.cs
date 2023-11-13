using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] String[] scenes;
    private bool canChange;
    private int i = 0;

    private void Start() {
        DontDestroyOnLoad(gameObject);
        canChange = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canChange) return;
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            i++;
            if(i >= scenes.Length) i = 0;
            Debug.Log("Loading " + scenes[i]);
            SceneManager.LoadScene(scenes[i]);
            canChange = false;
            Invoke("EnableChange", 1f);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            i--;
            if(i < 0) i = scenes.Length - 1;
            Debug.Log("Loading " + scenes[i]);
            SceneManager.LoadScene(scenes[i]);
            canChange = false;
            Invoke("EnableChange", 1f);
        }
    }

    private void EnableChange() {
        canChange = true;
    }
}
