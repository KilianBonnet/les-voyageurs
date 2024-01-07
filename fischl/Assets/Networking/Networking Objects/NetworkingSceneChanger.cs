using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkingSceneChanger : MonoBehaviour
{
    private void Start() {
        WebSocketClient.OnSocketMessage += OnSocketMessage;
    }

    private void OnSocketMessage(SocketMessage socketMessage) {
        if(socketMessage.op != SocketOP.SCENE_CHANGE_EVENT)
            return;
        SceneChangeData d = socketMessage.d.ToObject<SceneChangeData>();
        SceneManager.LoadScene(d.scene);
    }

    public static void SendSceneChangeEvent(string sceneName) {
        WebSocketClient.Instance.SendMessage(
            SocketOP.SCENE_CHANGE_EVENT, 
            new { scene = sceneName });
    }

}
