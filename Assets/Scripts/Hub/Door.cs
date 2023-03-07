using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, MovingObject
{
    private Rigidbody body;
    private Vector3 prevPos;
    private bool onDrag;
    private float currAngle;

    public float minAngle;
    public float maxAngle;

    public void MoveObject(Vector3 pos)
    {
        if (!onDrag)
        {
            prevPos = pos;
            onDrag = true;
            return;
        }

        float addAngle = pos.z - prevPos.z > 0? 1f : -1f;
        if(currAngle + addAngle > minAngle && currAngle + addAngle < maxAngle )
        {
            transform.eulerAngles += new Vector3(0, addAngle, 0);
            currAngle += addAngle;
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
        currAngle = 0;
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;
        if (onDrag)
        {
            GUILayout.Label("\n\n" + transform.localEulerAngles);
        }
    }
}
