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
            OnEnter();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Portal blue")
        {
            OnEnter();
        }
    }

    private void OnEnter()
    {

        GetComponent<Animator>().SetBool("isSentToTable", true);

        portal.GetComponent<Animator>().SetBool("isSentToTable", true);

        if (gameObject.name == "Emerald")
        {
            GetComponent<Animator>().SetBool("isEnemyDead", false);
            portal.GetComponent<Animator>().SetBool("canBeShown", false);
        }
        if (gameObject.name == "Bomb")
        {
            GetComponent<Animator>().SetBool("isLidOpen", false);
            portal.GetComponent<Animator>().SetBool("canBeShown", false);
        }
    }
}
