using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NetworkingSceneChanger.SendSceneChangeEvent("Bowrelia");
            SceneManager.LoadScene("Bowrelia");
        }
    }
}
