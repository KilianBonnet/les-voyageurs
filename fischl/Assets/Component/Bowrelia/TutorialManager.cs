using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

class ZoneMessage
{
    public bool isReady;
    public TextMeshProUGUI message;
}

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private string initMessage = "Cut the slime to start!";
    [SerializeField] private UnityEvent onTutorialComplete;

    private bool isVrReady;
    private ZoneMessage greenZone = new ZoneMessage();
    private ZoneMessage redZone = new ZoneMessage();
    private ZoneMessage blueZone = new ZoneMessage();
    private List<ZoneMessage> zones;

    private void Start()
    {
        Slime.OnDeath += SlimeDeathHandler;

        zones = new List<ZoneMessage>() {
            greenZone, blueZone, redZone
        };
        greenZone.message = GameObject.Find("Green Score").GetComponent<TextMeshProUGUI>();
        redZone.message = GameObject.Find("Red Score").GetComponent<TextMeshProUGUI>();
        blueZone.message = GameObject.Find("Blue Score").GetComponent<TextMeshProUGUI>();
        SetMessageToPlayers(initMessage);
    }

    public void OnVrReady()
    {
        isVrReady = true;
        HandleReadyPlayer();
    }

    private void SlimeDeathHandler(Transform slimeTransform, Cursor cursor)
    {
        switch (cursor.originalZone.name)
        {
            case "Red Zone":
                redZone.isReady = true;
                break;
            case "Blue Zone":
                blueZone.isReady = true;
                break;
            case "Green Zone":
                greenZone.isReady = true;
                break;
        }
        HandleReadyPlayer();
    }

    private void HandleReadyPlayer()
    {
        if (GetNbReadyPlayers() >= 4)
            onTutorialComplete.Invoke();
        else
            SetMessageToReadyPlayers("Ready: " + GetNbReadyPlayers() + "/4");
    }

    private void SetMessageToReadyPlayers(string message)
    {
        zones.FindAll(zone => zone.isReady)
            .ForEach(zone => zone.message.text = message);
    }

    private void SetMessageToPlayers(string message)
    {
        zones.ForEach(zone => zone.message.text = message);
    }

    private int GetNbReadyPlayers()
    {
        int res = isVrReady ? 1 : 0;
        zones.ForEach(zone => res += zone.isReady ? 1 : 0);
        return res;
    }
}
