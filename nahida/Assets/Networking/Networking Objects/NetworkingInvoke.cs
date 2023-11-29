using UnityEngine;
using UnityEngine.Events;

public class NetworkingInvoke : MonoBehaviour
{
    [SerializeField] private int networkObjectId;
    [SerializeField] private UnityEvent onNetworkInvoke;

    private void Start() {
        WebSocketClient.OnSocketMessage += OnSocketMessage;
    }

    private void OnSocketMessage(SocketMessage socketMessage) {
        if(socketMessage.op != SocketOP.INVOKE_EVENT)
            return;

        InvokeData d = socketMessage.d.ToObject<InvokeData>();
        if(d.networkObjectId == networkObjectId)
            onNetworkInvoke.Invoke();
    }

    public void SendInvokeEvent(int networkObjectId) {
        WebSocketClient.Instance.SendMessage(
            SocketOP.INVOKE_EVENT, 
            new { networkObjectId });
    }
}
