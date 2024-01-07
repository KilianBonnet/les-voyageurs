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
            if (d.position != null) transform.position = d.position.ToVector3();
            if (d.rotation != null) transform.rotation = Quaternion.Euler(d.rotation.ToVector3());
        }
    }

    private void Update()
    {
        if (!syncPosition && !syncRotation) return;

        TransformData payload = new TransformData()
        {
            networkObjectId = networkObjectId
        };
        if (syncPosition) payload.position = new SerializedVector3(transform.position);
        if (syncRotation) payload.rotation = new SerializedVector3(transform.rotation.eulerAngles);

        WebSocketClient.Instance.SendMessage(
            SocketOP.TRANSFORM_EVENT,
            payload
        );
    }
}
