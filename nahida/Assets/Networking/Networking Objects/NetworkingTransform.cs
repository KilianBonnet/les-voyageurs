using UnityEngine;

public class NetworkingTransform : MonoBehaviour
{
    [SerializeField] private int networkObjectId;
    [SerializeField] private bool syncPosition;
    [SerializeField] private bool syncRotation;

    private void Start()
    {
        WebSocketClient.OnSocketMessage += OnSocketMessage;
    }

    private void OnSocketMessage(SocketMessage socketMessage)
    {
        if (socketMessage.op != SocketOP.TRANSFORM_EVENT)
            return;

        TransformData d = socketMessage.d.ToObject<TransformData>();
        if (d.networkObjectId == networkObjectId)
        {
            if (d.position != null) transform.position = d.position.Value;
            if (d.rotation != null) transform.rotation = Quaternion.Euler(d.rotation.Value);
        }
    }

    private void Update()
    {
        if (!syncPosition && !syncRotation) return;

        TransformData payload = new();
        payload.networkObjectId = networkObjectId;
        if (syncPosition) payload.position = transform.position;
        if (syncRotation) payload.rotation = transform.rotation.eulerAngles;

        WebSocketClient.Instance.SendMessage(
            SocketOP.TRANSFORM_EVENT,
            payload
        );
    }
}
