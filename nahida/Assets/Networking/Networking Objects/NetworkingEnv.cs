
using UnityEngine;

public class NetworkingEnv : MonoBehaviour
{
    [SerializeField] Animator door;
    private void Start()
    {
        WebSocketClient.OnSocketMessage += OnSocketMessage;
    }

    private void OnSocketMessage(SocketMessage socketMessage)
    {
        Debug.Log("J'ai un message");
        if (socketMessage.op != SocketOP.SELECT_ENV_COMPLETE_EVENT)
            return;
        Debug.Log("Prout");
        door.SetBool("opening", true);
    }
}
