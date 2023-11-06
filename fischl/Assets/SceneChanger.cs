using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] String[] scenes;
    private int i = 0;

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            i++;
            if(i >= scenes.Length) i = 0;
            Debug.Log("Loading " + scenes[i]);
            SceneManager.LoadScene(scenes[i]);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            i--;
            if(i < 0) i = scenes.Length - 1;
            Debug.Log("Loading " + scenes[i]);
            SceneManager.LoadScene(scenes[i]);
        }
    }
}
