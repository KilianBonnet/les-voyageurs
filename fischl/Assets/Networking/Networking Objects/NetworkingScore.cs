using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ScoreUpdateEvent : UnityEvent<int> {}

public class NetworkingScore : MonoBehaviour
{
    [SerializeField] private ScoreUpdateEvent onScoreUpdate; 
    public int Score { get; private set; }

    private void Start() {
        WebSocketClient.OnSocketMessage += OnSocketMessage;
    }

    private void OnSocketMessage(SocketMessage socketMessage) {
        if(socketMessage.op != SocketOP.SCORE_EVENT)
            return;

        ScoreData d = socketMessage.d.ToObject<ScoreData>();
        if(d.type != "info")
            return;

        Score = d.score;
        onScoreUpdate.Invoke(Score);
    }

    public static void SendScoreEvent(int scoreIncrement) {
        WebSocketClient.Instance.SendMessage(
            SocketOP.SCORE_EVENT, 
            new { 
                type = "increase",
                score = scoreIncrement
            });
    }
}
