using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateDoor : MonoBehaviour
{
    public void Animate()
    {
        GetComponent<Animator>().SetBool("opening", true);
    }
}
