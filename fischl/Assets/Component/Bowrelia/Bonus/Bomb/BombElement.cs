using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BombState {
    IDLE,
    FALLING,
    EXPLODING
}

public class BombElement : Element
{
    private BombState bombState = BombState.IDLE;

    private float time;

    private const float TIME_TO_FALL = .5f;
    private float finalScale;

    private const float TIME_TO_EXPLODE = .5f;
    private float finalExplodingScale = 75;
    private Transform impactZone;

    public override void OnDropped()
    {
        bombState = BombState.FALLING;
        finalScale = transform.localScale.x / 2;
    }

    private void Update() {
        if(bombState != BombState.IDLE)
            time += Time.deltaTime;

        if(bombState == BombState.FALLING) {
            transform.localScale -= Vector3.one * (finalScale * Time.deltaTime / TIME_TO_FALL);
            if(time > TIME_TO_FALL) InitExploding();
        }

        if(bombState == BombState.EXPLODING) {
           impactZone.localScale += Vector3.one * (finalExplodingScale * Time.deltaTime / TIME_TO_EXPLODE);
           if(time > TIME_TO_EXPLODE) Destroy(gameObject);
        }
    }

    private void InitExploding() {
        bombState = BombState.EXPLODING;
        time = 0;
        GetComponent<SpriteRenderer>().enabled = false;

        impactZone = transform.GetChild(0);
        impactZone.gameObject.SetActive(true);
    }
}
