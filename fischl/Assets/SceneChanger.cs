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
        if(!Input.GetKeyDown(KeyCode.RightArrow))
            return;
        SceneManager.LoadScene(scenes[i]);
    }
}
