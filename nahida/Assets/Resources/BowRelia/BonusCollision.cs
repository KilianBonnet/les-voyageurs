using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCollision : MonoBehaviour
{
    private GameObject portal;
    
    void Start()
    {
        portal = transform.parent.Find("Portal blue").gameObject;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Portal blue")
        {
            GetComponent<Animator>().SetBool("isSentToTable", true);
            GetComponent<Animator>().SetBool("isLidOpen", false);
            portal.GetComponent<Animator>().SetBool("isSentToTable", true);
            portal.GetComponent<Animator>().SetBool("isLidOpen", false);
        }
    }
}
