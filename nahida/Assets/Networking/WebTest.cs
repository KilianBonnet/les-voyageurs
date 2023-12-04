using UnityEngine;

public class WebTest : MonoBehaviour
{
    public void DestroySelf() {
        Destroy(gameObject);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.S))
            NetworkingScore.SendScoreEvent(1);
    }
}
