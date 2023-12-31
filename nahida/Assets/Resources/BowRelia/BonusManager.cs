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

    public void Start()
    {
        bomb = transform.parent.Find("Bomb").gameObject;
        portal = transform.parent.Find("Portal blue").gameObject;
        text = transform.parent.Find("Canvas").Find("Text");
    }

    public void Update()
    {
        if(bomb)
        {
            if (bomb.GetComponent<Animator>().GetBool("isLidOpen"))
            {
                bomb.GetComponent<Grabbable>().enabled = true;
                bomb.GetComponent<HandGrabInteractable>().enabled = true;
                portal.SetActive(true);
                portal.GetComponent<Animator>().SetBool("isLidOpen", true);
                text.gameObject.SetActive(true);
            }

            if (bomb.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FadeIntoPortal") && bomb.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Destroy(bomb);
            }
        }
        if(portal && bomb == null)
        {
            portal.gameObject.GetComponent<Animator>().SetBool("isSentToTable", true);
            text.gameObject.SetActive(false);
            if(portal.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FadeIntoPortal") && portal.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Destroy(portal);
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
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Lid")
        {
            bomb.GetComponent<Animator>().SetBool("isLidOpen", false);
        }
        if(collision.gameObject.name == "Portal blue")
        {
            bomb.GetComponent<Animator>().SetBool("isSentToTable", true);
        }
    }
}
