using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrabbable : OVRGrabbable
{
    [SerializeField]
    private Transform _handle;
    Rigidbody _handleRB;
    private bool _isGrabbed = false;

    protected override void Start()
    {
        base.Start();
        _handleRB = _handle.GetComponent<Rigidbody>();
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        StartCoroutine(UpdateHandle());
        grabbedBy.GetComponentInChildren<Renderer>().enabled = false;
    }

    IEnumerator UpdateHandle()
    {
        _isGrabbed = true;
        while (_isGrabbed)
        {
            _handleRB.MovePosition(grabbedBy.transform.position);
            yield return null;
        }
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        _isGrabbed = false;
        transform.position = _handle.position;
        transform.rotation = _handle.rotation;
        grabbedBy.GetComponentInChildren<Renderer>().enabled = true;
        base.GrabEnd(linearVelocity, angularVelocity);
    }
}
