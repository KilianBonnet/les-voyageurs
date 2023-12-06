using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    private GameObject bomb;

    public void Start()
    {
        bomb = transform.parent.Find("Bomb").gameObject;
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
