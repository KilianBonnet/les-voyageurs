using UnityEngine;

public class NetworkingEnvPuzzle : MonoBehaviour
{
    public static void PuzzleComplete()
    {
        WebSocketClient.Instance.SendMessage(
            SocketOP.SELECT_ENV_COMPLETE_EVENT
            );
    }
}
