using UnityEngine;

public class SceneChange : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NetworkingSceneChanger.SendSceneChangeEvent("1");
        }
    }
}
