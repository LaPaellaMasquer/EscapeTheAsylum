using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHold : MonoBehaviour, MovingObject
{
    private Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void MoveObject(Vector3 pos)
    {
        body.freezeRotation = true;
        if ((pos - body.position).magnitude > 0.01)
            body.position = pos;
    }

    public void Release()
    {
        body.freezeRotation = false;
    }

}
