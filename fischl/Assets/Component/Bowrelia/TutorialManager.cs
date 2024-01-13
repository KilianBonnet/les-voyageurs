using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

class ZoneMessage
{
    public bool isReady;
    public bool isReady2;
    public TextMeshProUGUI message;
}

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private string initMessage = "Slice the slime !";
    [SerializeField] private string initMessage2 = "Put 2 fingers on the table then slide a 3rd one in between";

    [SerializeField] private UnityEvent onTutorialComplete;

    private bool isVrReady;
    private ZoneMessage greenZone = new ZoneMessage();
    private ZoneMessage redZone = new ZoneMessage();
    private ZoneMessage blueZone = new ZoneMessage();
    private List<ZoneMessage> zones;

    private bool firstTutorialDone = false;
    private bool secondTutorialDone = false;

    [SerializeField] private GameObject tuto2;

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
        if (!firstTutorialDone)
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
        }
        else
        {
            switch (cursor.originalZone.name)
            {
                case "Red Zone":
                    redZone.isReady2 = true;
                    break;
                case "Blue Zone":
                    blueZone.isReady2 = true;
                    break;
                case "Green Zone":
                    greenZone.isReady2 = true;
                    break;
            }
        }
        HandleReadyPlayer();
    }

    private void HandleReadyPlayer()
    {
        if(GetNbReadyPlayers() >= 4 && !secondTutorialDone)
        {
            secondTutorialDone = true;
        }
        if (!firstTutorialDone)
        {
            if (GetNbReadyPlayersTable() < 3)
            {
                SetMessageToReadyPlayers("Ready: " + GetNbReadyPlayersTable() + "/3");
            }
            else
            {
                firstTutorialDone = true;
                DisplaySecondPhase();
            }
        }
        
        else if (GetNbReadyPlayers() >= 4 && secondTutorialDone)
        {
            Slime.OnDeath -= SlimeDeathHandler;
            tuto2.SetActive(false);
            onTutorialComplete.Invoke();
        }
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
        zones.ForEach(zone => res += zone.isReady2 ? 1 : 0);
        return res;
    }

    private int GetNbReadyPlayersTable()
    {
        int res = 0;
        zones.ForEach(zone => res += zone.isReady ? 1 : 0);
        return res;
    }
    private void DisplaySecondPhase()
    {
        SetMessageToReadyPlayers(initMessage2);
        GameObject.Find("Tutorial Part 1").SetActive(false);
        tuto2.SetActive(true);
    }
}
