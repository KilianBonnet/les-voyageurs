using UnityEngine;
using UnityEngine.Events;

public class NetworkingTransform : MonoBehaviour
{
    [System.Serializable]
    public class PositionChangeEvent : UnityEvent<Vector3> { }
    [System.Serializable]
    public class RotationChangeEvent : UnityEvent<Vector3> { }

    [SerializeField] private int networkObjectId;
    [SerializeField] private bool syncPosition;
    [SerializeField] private bool syncRotation;
    [SerializeField] private PositionChangeEvent onPositionUpdate;
    [SerializeField] private RotationChangeEvent onRotationUpdate;

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
            if (d.position != null) onPositionUpdate.Invoke(d.position.ToVector3());
            if (d.rotation != null) onRotationUpdate.Invoke(d.rotation.ToVector3());
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
