using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, MovingObject
{
    private Rigidbody body;
    private HingeJoint joint;
    private Vector3 prevPos;
    private bool onDrag;

    public void MoveObject(Vector3 pos)
    {
        if (!onDrag)
        {
            prevPos = pos;
            onDrag = true;
            return;
        }

        Vector3 newRotation = body.rotation.eulerAngles;
        newRotation.y += pos.z - prevPos.z > 0? 1f : -1f;
        if(newRotation.y > joint.limits.min && newRotation.y < joint.limits.max)
        {
            body.rotation = Quaternion.Euler(newRotation);
        }
        prevPos = pos;
    }

    public void Release()
    {
        body.freezeRotation = false;
        onDrag = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.isKinematic = true;
        joint = GetComponent<HingeJoint>();
    }

    /*private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;
        if (onDrag)
        {
            GUILayout.Label("\n\n" + body.rotation.eulerAngles);
        }
    }*/
}
