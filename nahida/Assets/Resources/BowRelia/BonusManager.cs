using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using TMPro;

public class BonusManager : MonoBehaviour
{
    [SerializeField] RoomManager roomManager;
    private GameObject bomb;
    private GameObject portal;
    private Transform text;
    private ParticleSystem particleSystem;

    public void Start()
    {
        bomb = transform.parent.Find("Bomb").gameObject;
        portal = transform.parent.Find("Portal blue").gameObject;
        text = transform.parent.Find("Canvas").Find("Text");
        particleSystem = transform.parent.Find("Bomb").GetComponentInChildren<ParticleSystem>();
    }

    public void Update()
    {
        if (bomb != null && bomb.activeSelf)
        {
            if (bomb.GetComponent<Animator>().GetBool("isLidOpen"))
            {
                bomb.GetComponent<Grabbable>().enabled = true;
                bomb.GetComponent<HandGrabInteractable>().enabled = true;
                portal.SetActive(true);
                portal.GetComponent<Animator>().SetBool("canBeShown", true);
                text.gameObject.SetActive(true);
            }

            if (bomb.transform.localScale.x < 0.1f)
            {
                Destroy(bomb.gameObject);
            }
        }
        if (portal != null && portal.activeSelf)
        {
            if (bomb == null)
            {
                Destroy(portal.gameObject);
                Destroy(text.gameObject);
                roomManager.OpenDoors();
                NetworkingBonus.SendBonusEvent(BonusType.BOMB);
            }
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Lid")
        {
            bomb.GetComponent<Animator>().SetBool("isLidOpen", true);
            particleSystem.Play();

        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Lid")
        {
            bomb.GetComponent<Animator>().SetBool("isLidOpen", false);
        }
    }
}