using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorDeviceCamera : MonoBehaviour
{
    WebCamTexture mCamera = null;
    // Start is called before the first frame update
    void Start()
    {
        mCamera = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = mCamera;
        mCamera.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
