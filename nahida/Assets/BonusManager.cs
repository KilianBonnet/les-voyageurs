using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] RoomManager roomManager;
    private GameObject bomb;

    public void Start()
    {
        bomb = transform.parent.Find("Bomb").gameObject;
    }

    public void Update()
    {
        if(bomb)
        {
            if (bomb.GetComponent<Animator>().GetBool("isPositioned"))
            {
                Debug.Log("Send bomb to the table!");
                bomb.GetComponent<Animator>().SetBool("isSentToTable", true);
            }

            if (bomb.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("LiftUp") && bomb.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                bomb.GetComponent<Animator>().SetBool("isPositioned", true);
            }

            if (bomb.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FadeOut") && bomb.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Destroy(bomb);
                roomManager.OpenDoors();
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
    }
}
