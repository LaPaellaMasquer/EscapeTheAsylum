using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MovingObject
{
    public void MoveObject(Vector3 pos);

    public void Release();
}
