using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    [SerializeField] private GameObject disconnectedError;
    private WebSocketClient client;

    private GameObject progress;
    private GameObject bonus;
    private GameObject map;


    private void Start()
    {
        if(!disconnectedError)
            disconnectedError = GameObject.Find("Disconnected Error");
        client = GameObject.Find("Websocket Client").GetComponent<WebSocketClient>();

        progress = GameObject.Find("Progress");
        bonus = GameObject.Find("Bonus Notification");
        map = GameObject.Find("VRMap");
    }

    private void Update()
    {
        if(client.isReady)
            disconnectedError.SetActive(false);
        else
            disconnectedError.SetActive(true);
    }

    public void EndGameCanvas()
    {
        HideGameUI();
    }

    private void HideGameUI()
    {
        if(progress)
            progress.SetActive(false);
        if(bonus)
            bonus.SetActive(false);
        if(map)
            map.SetActive(false);
    }
}
