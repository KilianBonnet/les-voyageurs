using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombElement : Element
{
    public override void OnDropped()
    {
        Debug.Log("boom");
        Invoke("Test", 2f);
    }

    private void Test(){
        Destroy(gameObject);
    }
}
